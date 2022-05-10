using Jobsity.Chat.Application.ViewModels.ChatRoom;
using System;
using System.Threading.Tasks;

namespace Jobsity.Chat.Application.Interfaces.SignalR
{
    public interface IChatRoomMessageHub
    {
        public string GroupName { get; }

        Task JoinAsync(Guid chatRoomId);

        Task SendAsync(ChatRoomMessageViewModel chatMessage);

        Task LeaveAsync();
    }
}
