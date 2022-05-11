using Jobsity.Chat.Application.Handlers.Notifications.Stock;
using Jobsity.Chat.CrossCutting.Broker;
using MediatR;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Jobsity.Chat.Application.Broker
{
    public class StockBrokerService : BrokerHelper, IStockBrokerService
    {
        private readonly IMediator _mediator;

        public StockBrokerService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async void OnMessageReceived(object sender, BasicDeliverEventArgs mqMessage)
        {
            var body = mqMessage.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            await _mediator.Publish(new StockCodeProcessNotification(message, Guid.Parse("F632A1AF-CD05-4706-6E45-08DA310590DC")));
        }
    }
}
