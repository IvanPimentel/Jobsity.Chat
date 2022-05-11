using MediatR;
using System;

namespace Jobsity.Chat.Application.Handlers.Notifications.Stock
{
    public class StockCodeNotification : INotification
    {
        public string Message { get; set; }
        public Guid ChatRoomId { get; set; }

        public StockCodeNotification(string message, Guid chatRoomId)
        {
            Message = message;
            ChatRoomId = chatRoomId;
        }
    }
}
