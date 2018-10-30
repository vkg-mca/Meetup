using System.IO;

using Meetup.Entities.Models;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;

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
			//services.AddSwaggerGen(c =>
			//{
			//    c.SwaggerDoc("v1", new Info { Title = "Meetup API", Version = "v1" });
			//});

			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1",
					new Info
					{
						Title = "Meetup.Api",
						Version = "v1",
						Description = "Meetup API Services",
						TermsOfService = "WTFPL",
						Contact = new Contact
						{
							Email = "vkg.mca@gmail.com",
							Name = "Vinod Gupta",
							Url = "https://github.com/vkg-mca"
						}
					}
				);
				//services.ConfigureSwaggerGen(c => c.CustomSchemaIds(x => x.FullName));
				var xmlDocFile = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "Meetup.Api.xml");
				options.IncludeXmlComments(xmlDocFile);
				options.DescribeAllEnumsAsStrings();
			});



		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			};
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Meetup API V1");
			});
			app.UseMvc();
		}
	}
}
