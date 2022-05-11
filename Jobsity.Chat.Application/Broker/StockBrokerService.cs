using Jobsity.Chat.Application.Handlers.Notifications.Stock;
using Jobsity.Chat.CrossCutting.Broker;
using Jobsity.Chat.CrossCutting.Broker.Model;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
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
            var chatMessage = JsonConvert.DeserializeObject<ChatMessageBroker>(message);
            await _mediator.Publish(new StockCodeResponseNotification(chatMessage.Message, chatMessage.ChatRoomId));
        }
    }
}
