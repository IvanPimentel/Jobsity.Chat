using MediatR;
using System;

namespace Jobsity.Chat.StooqService.Handlers.Notifications.StockNotification
{
    public class StockRequestNotification : INotification
    {
        public string Message { get; private set; }

        public StockRequestNotification(string message)
        {
            Message = message;
            Validation();
        }

        private void Validation()
        {
            if(string.IsNullOrWhiteSpace(Message))
                throw new ArgumentNullException("message");
        }
    }
}
