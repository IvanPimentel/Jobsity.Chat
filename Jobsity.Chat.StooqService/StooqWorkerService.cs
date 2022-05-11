using Jobsity.Chat.CrossCutting.Broker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jobsity.Chat.StooqService
{
    public class StooqWorkerService : BackgroundService
    {
        private readonly ILogger<StooqWorkerService> _logger;
        private readonly IBroker _broker;
        private readonly IEnumerable<BrokerConfig> _brokerConfigs;

        public StooqWorkerService(
            ILogger<StooqWorkerService> logger,
            IBroker broker, IEnumerable<BrokerConfig> brokerConfigs)
        {
            _logger = logger;
            _broker = broker;
            _brokerConfigs = brokerConfigs;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                var requestBroker = _brokerConfigs.FirstOrDefault(b => b.Name == "RequestBroker");
                _broker.Receive(requestBroker);

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
