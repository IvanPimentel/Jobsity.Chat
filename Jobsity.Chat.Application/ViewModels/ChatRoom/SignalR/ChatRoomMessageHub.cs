using Jobsity.Chat.Application.Interfaces.SignalR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Jobsity.Chat.Application.ViewModels.ChatRoom.SignalR
{
    public class ChatRoomMessageHub : Hub, IChatRoomMessageHub
    {
        public string GroupName { get; private set; }

        public async Task JoinAsync(Guid chatRoomId)
        {
            GroupName = chatRoomId.ToString();
            await Groups.AddToGroupAsync(Context.ConnectionId, GroupName);
        }

        public async Task SendAsync(ChatRoomMessageViewModel chatMessage)
        {
            GroupName = chatMessage.ChatRoomId.ToString();
            await Clients.Group(GroupName).SendAsync("NewChatMessage", chatMessage);
            //await Clients.All.SendAsync("NewChatMessage", chatMessage);
        }

        public async Task LeaveAsync()
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, GroupName);
        }
    }
}
