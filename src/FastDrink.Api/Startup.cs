using FastDrink.Api.Extensions;
using FastDrink.Application;
using FastDrink.Infrastructure;
using FluentValidation.AspNetCore;
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
                     builder.WithOrigins(Configuration["CorsConfig:Customer"], Configuration["CorsConfig:Admin"]);
                 });
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
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
                MinimumSameSitePolicy = SameSiteMode.Lax,
                HttpOnly = HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.None
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
