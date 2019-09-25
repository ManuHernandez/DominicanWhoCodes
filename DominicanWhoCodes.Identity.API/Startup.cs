
using DominicanWhoCodes.Identity.API.Models;
using DominicanWhoCodes.Identity.API.TokenAuth;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Rewrite;
using System.IO;
using System;
using System.Collections.Generic;
using DominicanWhoCodes.Shared.ServiceDiscovery;
using System.Linq;
using IdentityServer4.Hosting;
using MassTransit;
using RabbitMQ.Client;

namespace DominicanWhoCodes.Identity.API
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
            ConfigureConsul(services);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);

            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("IdentityDb")));

            ConfigureAuth(services);
            ConfigureSwagger(services);
            ConfigureBus(services);
            services.AddHealthChecks();
        }

        private void ConfigureAuth(IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            var builder = services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryPersistedGrants()
                .AddInMemoryIdentityResources(IdentityServerTokenConfig.GetIdentityResources())
                .AddInMemoryApiResources(IdentityServerTokenConfig.GetApiResources())
                .AddInMemoryClients(IdentityServerTokenConfig.GetClients())
                .AddAspNetIdentity<User>();

            //Nice hack : https://stackoverflow.com/questions/39186533/change-default-endpoint-in-identityserver-4 :')
            builder.Services
            .Where(service => service.ServiceType == typeof(Endpoint))
            .Select(item => (Endpoint)item.ImplementationInstance)
            .ToList()
            .ForEach(item => item.Path = item.Path.Value.Replace("/connect", "/api"));
        }
        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new Info {
                    Title = "Identity API",
                    Version = "v1",
                    Description = "An API to manage users authentication and authorization " +
                    "of DominicanWhoCodes App"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);
            });
        }
        private void ConfigureConsul(IServiceCollection services)
        {
            var serviceCfg = Service.GetService(Configuration);
            services.RegisterConsulServices(serviceCfg);
        }
        private void ConfigureBus(IServiceCollection services)
        {
            services.AddMassTransit(m =>
            {
                m.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var host = cfg.Host(Configuration["RabbitMQ:Host"], Configuration["RabbitMQ:VirtualHost"],
                         cr =>
                         {
                             cr.Username(Configuration["RabbitMQ:UserName"]);
                             cr.Password(Configuration["RabbitMQ:Password"]);
                         });
                    cfg.ExchangeType = ExchangeType.Fanout;
                    
                }));
            });
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

            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "IdentityAPIv1");
                config.RoutePrefix = "swagger";
            });

            MigrateDb(app);

            app.UseIdentityServer();
            app.UseHttpsRedirection();

            app.UseHealthChecks("/heathcheck");
            app.UseMvc();
        }

        public void MigrateDb(IApplicationBuilder app)
        {
            using (var dbService = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = dbService.ServiceProvider.GetService<ApplicationDbContext>();
                if (dbContext != null) dbContext.Database.Migrate();
            }
        }
    }
}
