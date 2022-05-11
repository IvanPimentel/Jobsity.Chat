using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Jobsity.Chat.Application.Handlers.Notifications.Stock
{
    public class StockCodeNotificationHandler : INotificationHandler<StockCodeNotification>
    {
        public async Task Handle(StockCodeNotification notification, CancellationToken cancellationToken)
        {
            if (!cancellationToken.IsCancellationRequested)
            {
                //send to RabbitMQ to process
            }
        }
    }
}
