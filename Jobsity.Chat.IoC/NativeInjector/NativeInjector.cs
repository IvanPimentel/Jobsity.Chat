using Jobsity.Chat.Application.Interfaces;
using Jobsity.Chat.Application.Services;
using Jobsity.Chat.Data.Repository;
using Jobsity.Chat.Domain.Interfaces.Repositories;
using Jobsity.Chat.Domain.Interfaces.Services;
using Jobsity.Chat.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Jobsity.Chat.IoC.NativeInjector
{
    public static class NativeInjector
    {
        public static IServiceCollection AddDependiencies(this IServiceCollection services, Assembly assembly)
        {
            AppServices(services);
            DomainServices(services);
            Repositories(services);
            services.AddAutoMapper(assembly);
            return services;
        }

        private static void AppServices(IServiceCollection services)
        {
            services.AddScoped<IUserAppService, UserAppService>();
            services.AddScoped<IChatRoomAppService, ChatRoomAppService>();
        }

        private static void DomainServices(IServiceCollection services)
        {
            services.AddScoped<IChatRoomService, ChatRoomService>();
        }

        private static void Repositories(IServiceCollection services)
        {
            services.AddScoped<IChatRoomRepository, ChatRoomRepository>();
        }
    }
}
