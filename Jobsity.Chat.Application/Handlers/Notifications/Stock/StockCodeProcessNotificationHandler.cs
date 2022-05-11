using Jobsity.Chat.Application.Interfaces;
using Jobsity.Chat.Application.ViewModels.ChatRoom;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Jobsity.Chat.Application.Handlers.Notifications.Stock
{
    public class StockCodeProcessNotificationHandler : INotificationHandler<StockCodeProcessNotification>
    {
        private readonly IServiceProvider _serviceProvider;

        public StockCodeProcessNotificationHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Handle(StockCodeProcessNotification notification, CancellationToken cancellationToken)
        {
            if (!cancellationToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var _chatRoomMessageAppService = scope.ServiceProvider.GetRequiredService<IChatRoomMessageAppService>();
                    await _chatRoomMessageAppService.Create(new ChatRoomMessageViewModel(notification.Message, notification.ChatRoomId));
                }
            }
        }
    }
}
