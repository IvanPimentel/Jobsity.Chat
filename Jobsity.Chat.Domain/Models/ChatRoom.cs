using FluentValidation;
using FluentValidation.Results;
using Jobsity.Chat.Domain.Models.Base;
using System.Collections.Generic;

namespace Jobsity.Chat.Domain.Models
{
    public class ChatRoom : BaseModel
    {
        public string Name { get; private set; }
        public virtual List<ChatRoomMessage> ChatRoomMessages { get; set; }

        protected ChatRoom() { }

        public ChatRoom(string name)
        {
            Name = name;
        }

        public override bool IsValid()
        {
            ValidationResult = new ChatRoomValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class ChatRoomValidation : AbstractValidator<ChatRoom>
    {
        public ChatRoomValidation()
        {
            RuleFor(x => x.Name)
                .Length(3, 30)
                .WithMessage("Name of Chat Room must be greater than 3 characters and less than 30 characters");
        }
    }
}
