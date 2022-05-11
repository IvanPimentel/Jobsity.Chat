using MediatR;

namespace Jobsity.Chat.Application.Handlers.Notifications.Stock
{
    public class StockCodeNotification : INotification
    {
        public string Message { get; set; }

        public StockCodeNotification(string message)
        {
            Message = message;
        }
    }
}
