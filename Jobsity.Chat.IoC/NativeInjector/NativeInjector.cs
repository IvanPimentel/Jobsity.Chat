using Jobsity.Chat.Application.Interfaces;
using Jobsity.Chat.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Jobsity.Chat.IoC.NativeInjector
{
    public static class NativeInjector
    {
        public static IServiceCollection AddDependiencies(this IServiceCollection services)
        {
            AppServices(services);
            return services;
        }

        private static void AppServices(IServiceCollection services)
        {
            services.AddScoped<IUserAppService, UserAppService>();
        }
    }
}
