namespace Jobsity.Chat.CrossCutting.Broker
{
    public class BrokerConfig
    {
        public string Name { get; set; }
        public string HostName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
        public string QueueName { get; set; }

    }
}
