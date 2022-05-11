using Jobsity.Chat.CrossCutting.Broker;
using Jobsity.Chat.StooqService.Handlers.Notifications.StockNotification;
using MediatR;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client.Events;
using System.Text;

namespace Jobsity.Chat.StooqService.Broker
{
    public class BrokerService : BrokerHelper, IBroker
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BrokerService> _logger;

        public BrokerService(IMediator mediator, ILogger<BrokerService> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public override async void OnMessageReceived(object sender, BasicDeliverEventArgs mqMessage)
        {
            var body = mqMessage.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            _logger.LogInformation(" [x] Received {0}", message);
            await _mediator.Publish(new StockRequestNotification(message));
            
        }
    }
}
