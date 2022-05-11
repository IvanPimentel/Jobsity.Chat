using RabbitMQ.Client.Events;

namespace Jobsity.Chat.CrossCutting.Broker
{
    public interface IBroker
    {
        void Send(BrokerConfig brokerConfig, string message);
        public void Receive(BrokerConfig brokerConfig);
    }
}
