using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace MeetupApi
{
    public class DefaultConfigurationBuilder
    {
        public IConfigurationRoot Build(IWebHostEnvironment environment)
        {
            var target = Environment.GetEnvironmentVariable("TARGET");

            if (string.IsNullOrWhiteSpace(target)) target = "local";

            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings-{target}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}
