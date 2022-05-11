using Jobsity.Chat.Application.Interfaces;
using Jobsity.Chat.Application.ViewModels.ChatRoom;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Jobsity.Chat.Application.Handlers.Notifications.Stock
{
    public class StockCodeResponseNotificationHandler : INotificationHandler<StockCodeResponseNotification>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<StockCodeResponseNotificationHandler> _logger;

        public StockCodeResponseNotificationHandler(
            IServiceProvider serviceProvider, 
            ILogger<StockCodeResponseNotificationHandler> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task Handle(StockCodeResponseNotification notification, CancellationToken cancellationToken)
        {
            if (!cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Receive stock response message: {notification.Message} to chatRoom: {notification.ChatRoomId}");
                using (var scope = _serviceProvider.CreateScope())
                {
                    var _chatRoomMessageAppService = scope.ServiceProvider.GetRequiredService<IChatRoomMessageAppService>();
                    await _chatRoomMessageAppService.Create(new ChatRoomMessageViewModel(notification.Message, notification.ChatRoomId));
                    _logger.LogInformation($"Message processed");

                }
            }
        }
    }
}
