using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Jobsity.Chat.Application.ViewModels.ChatRoom.SignalR
{
    public class ChatRoomMessageHub : Hub
    {

        public async Task Join(Guid chatRoomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatRoomId.ToString());
        }

        public async Task Send(ChatRoomMessageViewModel chatMessage)
        {
            await Clients.Group(chatMessage.ChatRoomId.ToString()).SendAsync("NewChatMessage", chatMessage);
            //await Clients.All.SendAsync("NewChatMessage", chatMessage);
        }

        public async Task Leave(Guid chatRoomId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatRoomId.ToString());
        }
    }
}
