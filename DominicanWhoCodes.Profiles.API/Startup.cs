using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DominicanWhoCodes.Profiles.API.Application.IntegrationEvents.Consumers;
using DominicanWhoCodes.Profiles.API.Infrastructure.Modules;
using DominicanWhoCodes.Profiles.Infrastructure;
using DominicanWhoCodes.Shared.EventBus;
using DominicanWhoCodes.Shared.ServiceDiscovery;
using GreenPipes;
using MassTransit;
using MassTransit.RabbitMqTransport;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace DominicanWhoCodes.Profiles.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IBus BusManager { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddControllersAsServices();
            services.AddHostedService<BusService>();
            RegisterConsul(services);
            RegisterDbContext(services);
            RegisterCors(services);
            RegisterSwagger(services);
            var provider = InitializeAutofac(services);
            return provider;
        }

        private void RegisterConsul(IServiceCollection services)
        {
            var serviceCfg = Service.GetService(Configuration);
            services.RegisterConsulServices(serviceCfg);
        }   
        private void RegisterDbContext(IServiceCollection services)
        {
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<UserProfileContext>(options =>
                {
                    options.UseSqlServer(Configuration["ConnectionStrings:UserProfileDb"],
                        sqlServerOptionsAction: sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(typeof(Startup)
                                .GetTypeInfo()
                                .Assembly
                                .GetName()
                                .Name);
                            sqlOptions.EnableRetryOnFailure(maxRetryCount: 5,
                                maxRetryDelay: TimeSpan.FromSeconds(15),
                                errorNumbersToAdd: null);
                        });
                }, ServiceLifetime.Scoped);
        }
        private void RegisterCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }
        private void RegisterSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "User Profile API",
                    Version = "v1",
                    Description = "The User Profile HTTP API",
                    TermsOfService = "Terms Of Service"
                });
            });
        }
        private IServiceProvider InitializeAutofac(IServiceCollection services)
        {
            var container = new ContainerBuilder();
            container.Populate(services);
            container.RegisterModule(new MediatorModule(Configuration));
            container.RegisterModule(new ApplicationModule(Configuration));
            var provider = new AutofacServiceProvider(container.Build());
            return provider;
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
            app.UseCors("CorsPolicy");
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "UserProfileAPIv1");
                config.RoutePrefix = "";
            });
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
