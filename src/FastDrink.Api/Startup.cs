﻿using FastDrink.Api.Extensions;
using FastDrink.Application;
using FastDrink.Infrastructure;
using FluentValidation.AspNetCore;
using HashidsNet;
using Microsoft.AspNetCore.CookiePolicy;
using System.Text.Json.Serialization;

namespace FastDrink.Api
{
    public class Startup
    {
        private const string CorsPolicyName = "_fastDrinkWebs";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers()
                .AddFluentValidation()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });

            services.AddApplication(Configuration);
            services.AddInfrastructure(Configuration);

            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicyName, builder =>
                 {
                     builder.WithOrigins(Configuration["CorsConfig:Customer"], Configuration["CorsConfig:Admin"])
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                 });
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddSingleton<IHashids>(_ => new Hashids(Configuration["Hashids"], 11));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }

            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseCors(CorsPolicyName);

            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.None,
                HttpOnly = HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.Always
            });
            app.UseSecureJwt();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
