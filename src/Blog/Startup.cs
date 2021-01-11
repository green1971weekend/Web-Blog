using Blog.Application;
using Blog.Infrastructure;
using Blog.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Blog
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfrastructure();

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(_config["DefaultConnection"]));

            // Since by default Identity does not have redirect url for the not authorized users we have to override its behavior here.
            // Cookies is responsible for authentication hence the ConfigureApplicationCookie method is needed.
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Auth/Login";
            });

            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
                options.CacheProfiles.Add("Monthly", new CacheProfile { Duration = 60 * 60 * 24 * 7 * 4 });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();
        }
    }
}
