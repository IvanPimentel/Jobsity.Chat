using Jobsity.Chat.Application.Broker;
using Jobsity.Chat.CrossCutting.Broker;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jobsity.Chat.Application.Services.HostedServices
{
    public class StockCodeHostedService : BackgroundService
    {
        private readonly IStockBrokerService _stockBrokerService;
        private readonly IEnumerable<BrokerConfig> _brokerConfigs;

        public StockCodeHostedService(
            IStockBrokerService stockBrokerService, 
            IEnumerable<BrokerConfig> brokerConfigs)
        {
            _stockBrokerService = stockBrokerService;
            _brokerConfigs = brokerConfigs;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!stoppingToken.IsCancellationRequested)
            {
                var responseBroker = _brokerConfigs.FirstOrDefault(b => b.Name == "ResponseBroker");
                await Task.Run(() => _stockBrokerService.Receive(responseBroker));
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
