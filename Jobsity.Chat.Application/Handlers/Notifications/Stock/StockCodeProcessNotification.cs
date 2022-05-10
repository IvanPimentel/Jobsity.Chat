using MediatR;
using System;

namespace Jobsity.Chat.Application.Handlers.Notifications.Stock
{
    public class StockCodeProcessNotification : INotification
    {
        public string Message { get; set; }
        public Guid ChatRoomId { get; set; }

        public StockCodeProcessNotification(string message, Guid chatRoomId)
        {
            Message = message;
            ChatRoomId = chatRoomId;
        }
    }
}
