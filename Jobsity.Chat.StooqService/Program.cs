using Jobsity.Chat.CrossCutting.Broker;
using Jobsity.Chat.StooqService.Broker;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Reflection;

namespace Jobsity.Chat.StooqService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration configuration = hostContext.Configuration;
                    IEnumerable<BrokerConfig> options = configuration.GetSection("Brokers").Get<IEnumerable<BrokerConfig>>();
                    services.AddSingleton(options);
                    services.AddHostedService<StooqWorkerService>();
                    services.AddSingleton<IBroker, BrokerService>();
                    services.AddMediatR(Assembly.GetExecutingAssembly());
                });
    }
}
