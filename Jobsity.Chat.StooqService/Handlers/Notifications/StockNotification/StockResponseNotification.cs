using Jobsity.Chat.CrossCutting.Broker.Model;
using Jobsity.Chat.StooqService.Model;
using MediatR;
using System;

namespace Jobsity.Chat.StooqService.Handlers.Notifications.StockNotification
{
    public class StockResponseNotification : INotification
    {
        public Stock Stock { get; private set; }
        public ChatMessageBroker ChatMessageBroker { get; set; }
        public bool Success { get; private set; }
        


        public StockResponseNotification(Stock stock, ChatMessageBroker chatMessageBroker)
        {
            Stock = stock;
            ChatMessageBroker = chatMessageBroker;
            Validation();
        }

        private void Validation()
        {
            Success = Stock != null;
            if(Stock != null && Stock.Close == "N/D")
                Success = false;
        }
    }
}
