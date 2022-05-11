using Jobsity.Chat.CrossCutting.Broker;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jobsity.Chat.StooqService.Handlers.Notifications.StockNotification
{
    public class StockResponseNotificationHandler : INotificationHandler<StockResponseNotification>
    {
        private readonly IBroker _broker;
        private readonly ILogger<StockResponseNotificationHandler> _logger;
        private readonly IEnumerable<BrokerConfig> _brokerConfigs;


        public StockResponseNotificationHandler(
            IBroker broker, 
            ILogger<StockResponseNotificationHandler> logger, 
            IEnumerable<BrokerConfig> brokerConfigs)
        {
            _broker = broker;
            _logger = logger;
            _brokerConfigs = brokerConfigs;
        }

        public Task Handle(StockResponseNotification notification, CancellationToken cancellationToken)
        {
            if (!cancellationToken.IsCancellationRequested)
            {
                var responseBroker = _brokerConfigs.FirstOrDefault(b => b.Name == "ResponseBroker");
                if (notification.Success)
                {
                    _logger.LogInformation("Sending data response to broker");
                    

                    _broker.Send(responseBroker, MakeMessageResponse(notification));
                    _logger.LogInformation("Success to process stock code");
                }
                else
                {
                    _logger.LogError("Error on process response");
                    _broker.Send(responseBroker, MakeErrorMessageResponse(notification));
                }
            }
            return Task.CompletedTask;
        }

        private static string MakeMessageResponse(StockResponseNotification notification)
        {
            return $"{notification.Stock.Symbol} quote is ${notification.Stock.Close} per share";
        }

        private static string MakeErrorMessageResponse(StockResponseNotification notification)
        {
            return $"Quote: {notification.StockCode} is invalid or not found";
        }
    }
}
