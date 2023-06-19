using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Meetup.Entities.Models;
using Meetup.Repository;
using Microsoft.Extensions.Configuration;

namespace MeetupApi
{
    public class MeetupBootstrapper : Module
    {
        private readonly IConfiguration _configuration;

        public MeetupBootstrapper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MeetupDbContext>();
            builder.RegisterType<MeetupDetailRepository>().As<IMeetupDetailRepository>().SingleInstance();
        }
    }
}
