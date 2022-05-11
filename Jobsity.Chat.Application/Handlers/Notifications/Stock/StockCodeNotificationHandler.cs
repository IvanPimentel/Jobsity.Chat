using Jobsity.Chat.Application.Broker;
using Jobsity.Chat.CrossCutting.Broker;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jobsity.Chat.Application.Handlers.Notifications.Stock
{
    public class StockCodeNotificationHandler : INotificationHandler<StockCodeNotification>
    {
        private readonly ILogger<StockCodeNotificationHandler> _logger;
        private readonly IStockBrokerService _stockBrokerService;
        private readonly IEnumerable<BrokerConfig> _brokerConfigs;

        public StockCodeNotificationHandler(ILogger<StockCodeNotificationHandler> logger, IStockBrokerService stockBrokerService, IEnumerable<BrokerConfig> brokerConfigs)
        {
            _logger = logger;
            _stockBrokerService = stockBrokerService;
            _brokerConfigs = brokerConfigs;
        }

        public Task Handle(StockCodeNotification notification, CancellationToken cancellationToken)
        {
            if (!cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Sending stock code command: {notification.Message}");
                var requestBroker = _brokerConfigs.FirstOrDefault(b => b.Name == "RequestBroker");
                var stringMessage = Newtonsoft.Json.JsonConvert.SerializeObject(notification);
                _stockBrokerService.Send(requestBroker, stringMessage);
                _logger.LogInformation($"Stock code sent successfully: {notification.Message}");

            }
            return Task.CompletedTask;
        }
    }
}
