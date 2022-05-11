using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Jobsity.Chat.CrossCutting.Broker
{
    public abstract class BrokerHelper : IBroker
    {

        public void Send(BrokerConfig brokerConfig, string message)
        {
            using (var connection = GetConnectionFactory(brokerConfig).CreateConnection())
            using (var channel = connection.CreateModel())
            {
                DeclareQueue(brokerConfig.QueueName, channel);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: brokerConfig.QueueName,
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }
        }

        public void Receive(BrokerConfig brokerConfig)
        {
            using (var connection = GetConnectionFactory(brokerConfig).CreateConnection())
            using (var channel = connection.CreateModel())
            {
                DeclareQueue(brokerConfig.QueueName, channel);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += OnMessageReceived;
                channel.BasicConsume(queue: brokerConfig.QueueName,
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }

        public abstract void OnMessageReceived(object sender, BasicDeliverEventArgs mqMessage);

        private static ConnectionFactory GetConnectionFactory(BrokerConfig brokerConfig) =>
             new ConnectionFactory() { HostName = brokerConfig.HostName, Password = brokerConfig.Password, UserName = brokerConfig.Username };

        private static void DeclareQueue(string queueName, IModel channel) =>
            channel.QueueDeclare(queue: queueName,
                                             durable: false,
                                             exclusive: false,
                                             autoDelete: false,
                                             arguments: null);
    }
}
