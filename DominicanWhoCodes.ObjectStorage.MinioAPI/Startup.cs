using Autofac;
using Autofac.Extensions.DependencyInjection;
using DominicanWhoCodes.ObjectStorage.MinioAPI.Models.Application.Command;
using DominicanWhoCodes.ObjectStorage.MinioAPI.Models.Application.IntegrationEvents.Consumers;
using DominicanWhoCodes.Shared.EventBus;
using GreenPipes;
using MassTransit;
using MassTransit.AutofacIntegration;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using RabbitMQ.Client;
using System;
using System.Reflection;

namespace DominicanWhoCodes.ObjectStorage.MinioAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddHostedService<BusService>();
            ConfigureMinioClient(services);
            return InitializeAutofac(services);
        }

        private void ConfigureMinioClient(IServiceCollection services)
        {
            string server = Configuration["MinioConfiguration:Server"];
            string accessKey = Configuration["MinioConfiguration:AccessKey"];
            string secretKey = Configuration["MinioConfiguration:SecretKey"];
            bool withSsl = bool.Parse(Configuration["MinioConfiguration:WithSSL"].ToString());
            if (withSsl)
                services.AddTransient((x) => 
                        new MinioClient(endpoint: server, 
                                        accessKey: accessKey, 
                                        secretKey: secretKey).WithSSL());
            else
                services.AddTransient((x) =>
                        new MinioClient(endpoint: server,
                                        accessKey: accessKey,
                                        secretKey: secretKey));
        }

        private IServiceProvider InitializeAutofac(IServiceCollection services)
        {
            var container = new ContainerBuilder();
            container.Populate(services);

            container.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
               .AsImplementedInterfaces();

            container.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            });

            container.RegisterConsumers(typeof(UploadProfilePhotoIntegrationEventHandler)
              .GetTypeInfo().Assembly).AsSelf().AsImplementedInterfaces();

            container.RegisterAssemblyTypes(typeof(UploadProfilePhotoCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            container.RegisterGeneric(typeof(AutofacConsumerFactory<>))
                .WithParameter(new NamedParameter("name", "message"))
                .As(typeof(IConsumerFactory<>))
                .InstancePerLifetimeScope();

            LoadEventBusConfiguration(container);

            var provider = new AutofacServiceProvider(container.Build());
            return provider;
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

                    string uniqueQueue = "Storage.ProfilePhotos";
                    r.ReceiveEndpoint(host, uniqueQueue, e =>
                    {
                        e.PrefetchCount = 1;
                        e.UseMessageRetry(ret => ret.Interval(retryCount: 5, interval: 6000));
                        e.Consumer<UploadProfilePhotoIntegrationEventHandler>(c);
                    });
                });

            })
            .As<IBusControl>()
            .As<IBus>()
            .As<IPublishEndpoint>()
            .SingleInstance()
            .AutoActivate();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Minio Object Storage API is Running...");
            });
        }
    }
}
