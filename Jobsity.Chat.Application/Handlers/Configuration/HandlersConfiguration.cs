using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Jobsity.Chat.Application.Handlers.Configuration
{
    public static class HandlersConfiguration
    {
        public static IServiceCollection ConfigureMediatR(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
