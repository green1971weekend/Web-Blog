using Blog.Application.Interfaces;
using Blog.Infrastructure.Data;
using Blog.Infrastructure.Persistence;
using Blog.Infrastructure.Services.IdentityService;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Infrastructure
{
    /// <summary>
    /// Defines an extension method for services addition of Infrastructure layer.
    /// </summary>
    public static class InfrastructureDependencyInjection
    {
        /// <summary>
        /// Extension method for services addition of Infrastructure layer.
        /// </summary>
        /// <param name="services">DI container.</param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = ".@abcdefghijklmnopqrstuvwxyz1234567890";
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 6;
            })
                .AddEntityFrameworkStores<AppDbContext>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IFileManager, FileManager>();
            services.AddScoped<IPostRepositoryExtension, PostRepositoryExtension>();
            services.AddScoped<IIdentityService, IdentityService>();

            return services;
        }
    }
}
