using Autofac;
using Meetup.Entities.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;

namespace MeetupApi
{
    public class Startup
    {
        private IWebHostEnvironment Environment { get; }
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
	 Environment = environment;

	 Configuration = new DefaultConfigurationBuilder().Build(environment);
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
	 services.AddMvc();
	 services.AddDbContext<MeetupDbContext>(options =>
	     options.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=MeetupDb;Integrated Security=SSPI;TrustServerCertificate=True;"));
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
		      Url = new Uri("https://github.com/vkg-mca")
		  }
	         }
	     );
	     //services.ConfigureSwaggerGen(c => c.CustomSchemaIds(x => x.FullName));
	     var xmlDocFile = Path.Combine(Directory.GetCurrentDirectory(), "MeetupApi.xml");
	     options.IncludeXmlComments(xmlDocFile);
	 });

	 services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
	 if (env.IsDevelopment())
	 {
	     app.UseDeveloperExceptionPage();
	 }

	 app.UseHttpsRedirection();

	 app.UseRouting();

	 app.UseAuthorization();

	 app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
	 app.UseSwagger();
	 app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Meetup API - V1"); });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
	 builder.RegisterModule(new MeetupBootstrapper(Configuration));
        }
    }
}
