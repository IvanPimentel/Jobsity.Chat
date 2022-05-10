using AutoMapper;
using Jobsity.Chat.Application.AutoMapper;
using Jobsity.Chat.Application.Interfaces;
using Jobsity.Chat.Application.Interfaces.SignalR;
using Jobsity.Chat.Application.Services;
using Jobsity.Chat.Application.ViewModels.ChatRoom.SignalR;
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
            ConfigureAutoMapper(services);
            AppServices(services);
            DomainServices(services);
            Repositories(services);
            return services;
        }

        private static void ConfigureAutoMapper(IServiceCollection services)
        {
            IMapper mapper = AutoMapperConfig.RegisterMappings().CreateMapper();
            services.AddSingleton(mapper);
        }

        private static void AppServices(IServiceCollection services)
        {
            services.AddScoped<IUserAppService, UserAppService>();
            services.AddScoped<IChatRoomAppService, ChatRoomAppService>();
            services.AddScoped<IChatRoomMessageAppService, ChatRoomMessageAppService>();
            services.AddSingleton<IChatRoomMessageHub, ChatRoomMessageHub>();
        }

        private static void DomainServices(IServiceCollection services)
        {
            services.AddScoped<IChatRoomService, ChatRoomService>();
            services.AddScoped<IChatRoomMessageService, ChatRoomMessageService>();
        }

        private static void Repositories(IServiceCollection services)
        {
            services.AddScoped<IChatRoomRepository, ChatRoomRepository>();
            services.AddScoped<IChatRoomMessageRepository, ChatRoomMessageRepository>();
        }
    }
}
