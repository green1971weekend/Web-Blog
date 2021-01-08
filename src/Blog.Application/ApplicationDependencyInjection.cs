using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Blog.Application
{
    /// <summary>
    /// Defines an extension method for services addition of Application layer.
    /// </summary>
    public static class ApplicationDependencyInjection
    {
        /// <summary>
        /// Extension method for services addition of Application layer.
        /// </summary>
        /// <param name="services">DI container.</param>
        /// <returns></returns>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}
