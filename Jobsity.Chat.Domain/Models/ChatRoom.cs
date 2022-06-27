using FluentValidation.Results;
using Jobsity.Chat.Domain.Models.Base;
using Jobsity.Chat.Domain.Models.Validators;
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
            ValidationResult = new ChatRoomValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
