using Jobsity.Chat.Application.Interfaces;
using Jobsity.Chat.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Jobsity.Chat.IoC.NativeInjector
{
    public static class NativeInjector
    {
        public static IServiceCollection AddDependiencies(this IServiceCollection services, Assembly assembly)
        {
            AppServices(services);
            services.AddAutoMapper(assembly);
            return services;
        }

        private static void AppServices(IServiceCollection services)
        {
            services.AddScoped<IUserAppService, UserAppService>();
        }
    }
}
