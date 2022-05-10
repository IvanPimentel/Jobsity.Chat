using Jobsity.Chat.Application.ViewModels.Base;
using System;

namespace Jobsity.Chat.Application.ViewModels.ChatRoom
{
    public class ChatRoomMessageViewModel : BaseViewModel
    {
        public string Content { get; set; }
        public Guid UserId { get; set; }
        public Guid ChatRoomId { get; set; }
        public virtual ChatRoomViewModel ChatRoom { get; set; }
        public ChatRoomMessageViewModel()
        {
        }

        public ChatRoomMessageViewModel(string content, Guid chatRoomId)
        {
            Content = content;
            ChatRoomId = chatRoomId;
        }
    }
}
