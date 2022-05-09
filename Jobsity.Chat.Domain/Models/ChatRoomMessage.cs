using Jobsity.Chat.Domain.Class;
using Jobsity.Chat.Domain.Models.Base;
using System;

namespace Jobsity.Chat.Domain.Models
{
    public class ChatRoomMessage : BaseModel
    {
        public string Content { get; private set; }
        public Guid UserId { get; private set; }
        public virtual User User { get; private set; }
        public Guid ChatRoomId { get; private set; }
        public virtual ChatRoom ChatRoom { get; private set; }

        protected ChatRoomMessage() { }

        public ChatRoomMessage(string content, Guid userId, Guid chatRoomId)
        {
            Content = content;
            UserId = userId;
            ChatRoomId = chatRoomId;
            Valitation();
        }

        protected override void Valitation()
        {
            if (Content == null || Content.Length < 1 || Content.Length > 100)
                throw new DomainExeption("Name of Chat Room must be greater than 0 characters and less than 100 characters");
            if (UserId == Guid.Empty)
                throw new DomainExeption("UserId is required");
            if (ChatRoomId == Guid.Empty)
                throw new DomainExeption("ChatRoomId is required");
        }
    }
}
