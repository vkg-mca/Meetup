using System;
using System.IO;
using System.Threading.Tasks;

using BeatPulse;
using BeatPulse.Core;
using BeatPulse.UI;

using Meetup.Entities.Models;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace Meetup.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<MeetupDbContext>(options => options.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=MeetupDb;Integrated Security=SSPI;"));
            
            //Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Meetup Api",
                        Version = "v1",
                        Description = "Meetup API Services",
                        Contact = new OpenApiContact
                        {
                            Email = "vkg.mca@gmail.com",
                            Name = "Vinod Gupta",
                            Url = new Uri( "https://github.com/vkg-mca")
                        }
                    }
                );
                //services.ConfigureSwaggerGen(c => c.CustomSchemaIds(x => x.FullName));
                var xmlDocFile = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "Meetup.Api.xml");
                options.IncludeXmlComments(xmlDocFile);
                options.DescribeAllEnumsAsStrings();
            });

            //Beatpulse
            services.AddBeatPulse(setup =>
            {
                //
                //add existing liveness packages
                //

                setup.AddSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=MeetupDb;Integrated Security=SSPI;");
                // or setup.AddXXXX() for all liveness packages on Nuget (mysql,sqlite,urlgroup,redis,idsvr,kafka,aws dynamo,azure storage and much more)
                // ie: setup.AddOracle("Data Source=localhost:49161/xe;User Id=system;Password=oracle");

                setup.AddUrlGroup(new Uri[] { new Uri("http://www.google.es") });

                //setup.AddUrlGroup(opt =>
                //{
                //    opt.AddUri(new Uri("http://google.com"), uri =>
                //    {
                //        uri.UsePost()
                //           .AddCustomHeader("X-Method-Override", "DELETE");
                //    });
                //}, "uri-group2", "UriLiveness2");

                //
                //create simple ad-hoc liveness
                //

                setup.AddLiveness("custom-liveness", opt =>
                {
                    opt.UsePath("custom-liveness");
                    opt.UseLiveness(new ActionLiveness((cancellationToken) =>
                    {
                        if (DateTime.Now.Minute % 3 == 0)
                        {
                            return Task.FromResult(
                                LivenessResult.Healthy());
                        }
                        else
                        {
                            return Task.FromResult(
                                LivenessResult.UnHealthy(new ArgumentNullException("param1")));
                        }
                    }));
                });

                //
                //ceate ad-hoc liveness with dependency resolution
                //

                //setup.AddLiveness("custom-liveness-with-dependency", opt =>
                //{
                //    opt.UsePath("custom-liveness-with-dependency");
                //    opt.UseFactory(sp => new ActionLiveness((cancellationToken) =>
                //    {
                //        var logger = sp.GetRequiredService<ILogger<Startup>>();
                //        logger.LogInformation("Logger is a dependency for this liveness");

                //        return Task.FromResult(
                //            LivenessResult.Healthy());
                //    }));
                //});
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            };
            app.UseBeatPulseUI();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Meetup API V1");
            });
        }
    }
}


