using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Meetup.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
	 CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
	  Host.CreateDefaultBuilder(args)
	      .UseServiceProviderFactory(new AutofacServiceProviderFactory())
	      .ConfigureWebHostDefaults(webBuilder =>
	      {
	          webBuilder.UseKestrel(options => options.AllowSynchronousIO = true)
	          .UseStartup<Startup>();
	      });
    }
}
