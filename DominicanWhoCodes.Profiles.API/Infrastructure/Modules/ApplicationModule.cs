using Autofac;
using DominicanWhoCodes.Profiles.Domain.Aggregates.Users;
using DominicanWhoCodes.Profiles.Infrastructure.Repositories;
using MassTransit;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace DominicanWhoCodes.Profiles.API.Infrastructure.Modules
{
    public class ApplicationModule : Module
    {
        private readonly IConfiguration _configuration;
        public ApplicationModule(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserRepository>()
                .As<IUserRepository>()
                .InstancePerLifetimeScope();
            LoadEventBusConfiguration(builder);
        }

        private void LoadEventBusConfiguration(ContainerBuilder builder)
        {
            builder.Register(c =>
            {
                return Bus.Factory.CreateUsingRabbitMq(r =>
                {
                    r.Host(_configuration["RabbitMQ:Host"], _configuration["RabbitMQ:VirtualHost"],
                        cr =>
                        {
                            cr.Username(_configuration["RabbitMQ:UserName"]);
                            cr.Password(_configuration["RabbitMQ:Password"]);
                        });
                    r.ExchangeType = ExchangeType.Fanout;
                });
            })
            .As<IBusControl>()
            .As<IBus>()
            .As<IPublishEndpoint>()
            .SingleInstance();
        }
    }
}
