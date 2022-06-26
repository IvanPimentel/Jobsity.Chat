using FluentValidation;
using Jobsity.Chat.Domain.Models.Base;
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

    public class ChatRoomMessageValidator : AbstractValidator<ChatRoomMessage>
    {
        public ChatRoomMessageValidator()
        {
            RuleFor(x => x.ChatRoomId).NotEqual(Guid.Empty)
                .WithMessage("ChatRoomId is required");

            RuleFor(x => x.UserId).NotEqual(Guid.Empty)
                .When(x => !x.Integration)
                .WithMessage("UserId is required");

            RuleFor(x => x.Content)
                .Length(1, 100)
                .WithMessage("Message must be greater than 0 characters and less than 100 characters");
        }
    }
}
