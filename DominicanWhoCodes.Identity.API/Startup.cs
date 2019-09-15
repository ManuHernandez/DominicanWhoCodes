
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);

            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("IdentityDb")));

            ConfigureAuth(services);
            ConfigureSwagger(services);
        }

        private void ConfigureAuth(IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryPersistedGrants()
                .AddInMemoryIdentityResources(IdentityServerTokenConfig.GetIdentityResources())
                .AddInMemoryApiResources(IdentityServerTokenConfig.GetApiResources())
                .AddInMemoryClients(IdentityServerTokenConfig.GetClients())
                .AddAspNetIdentity<User>();
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
            app.UseIdentityServer();
            app.UseHttpsRedirection();

            app.UseMvc();
        }
    }
}
