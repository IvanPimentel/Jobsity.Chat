using Jobsity.Chat.Domain.Models.Base;
using Jobsity.Chat.Domain.Models.Validators;
using System;

namespace Jobsity.Chat.Domain.Models
{
    public class ChatRoomMessage : BaseModel
    {
        public string Content { get; private set; }
        public Guid? UserId { get; private set; }
        public virtual User User { get; private set; }
        public Guid ChatRoomId { get; private set; }
        public bool Integration { get; set; } = false;

        public virtual ChatRoom ChatRoom { get; private set; }

        protected ChatRoomMessage() { }

        public ChatRoomMessage(string content, Guid? userId, Guid chatRoomId, bool integration = false)
        {
            Content = content;
            UserId = userId;
            ChatRoomId = chatRoomId;
            Integration = integration;
        }

        public bool IsStockCode()
        {
            return Content.StartsWith("/stock=");
        }

        public override bool IsValid()
        {
            ValidationResult = new ChatRoomMessageValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
