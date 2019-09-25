using Autofac;
using DominicanWhoCodes.Profiles.API.Application.IntegrationEvents.Consumers;
using DominicanWhoCodes.Profiles.Domain.Aggregates.Users;
using DominicanWhoCodes.Profiles.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using GreenPipes;
using MassTransit;
using MassTransit.AutofacIntegration;
using System.Reflection;
using RabbitMQ.Client;

namespace DominicanWhoCodes.Profiles.API.Infrastructure.Modules
{
    public class ApplicationModule : Autofac.Module
    {
        public IConfiguration Configuration { get; private set; }

        public ApplicationModule(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserRepository>()
                .As<IUserRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterConsumers(typeof(ApplicationModule)
                .GetTypeInfo().Assembly).AsSelf().AsImplementedInterfaces();

            builder.RegisterGeneric(typeof(AutofacConsumerFactory<>))
                .WithParameter(new NamedParameter("name", "message"))
                 .As(typeof(IConsumerFactory<>))
                 .InstancePerLifetimeScope();

            LoadEventBusConfiguration(builder);
        }

        private void LoadEventBusConfiguration(ContainerBuilder builder)
        {
            builder.Register((c) =>
            {
                return Bus.Factory.CreateUsingRabbitMq(r =>
                {
                    var host = r.Host(Configuration["RabbitMQ:Host"], Configuration["RabbitMQ:VirtualHost"],
                        cr =>
                        {
                            cr.Username(Configuration["RabbitMQ:UserName"]);
                            cr.Password(Configuration["RabbitMQ:Password"]);
                        });
                    r.ExchangeType = ExchangeType.Fanout;

                    string uniqueQueue = "Profile.CreateNewProfile";
                    r.ReceiveEndpoint(host, uniqueQueue, e =>
                    {
                        e.PrefetchCount = 1;
                        e.UseMessageRetry(ret => ret.Interval(retryCount: 3, interval: 3000));
                        e.Consumer<CreateNewUserProfileIntegrationEventHandler>(c);
                    });
                });

            })
            .As<IBusControl>()
            .As<IBus>()
            .As<IPublishEndpoint>()
            .SingleInstance()
            .AutoActivate();
        }
    }
}
