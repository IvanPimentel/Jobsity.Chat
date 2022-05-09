using Jobsity.Chat.Domain.Class;
using Jobsity.Chat.Domain.Models.Base;
using System;

namespace Jobsity.Chat.Domain.Models
{
    public class ChatRoom : BaseModel
    {
        public string Name { get; private set; }

        protected ChatRoom() { }

        public ChatRoom(string name)
        {
            Name = name;
            Valitation();
        }

        protected override void Valitation()
        {
            if (Name == null || Name.Length < 3 || Name.Length > 30)
                throw new DomainExeption("Name of Chat Room must be greater than 3 characters and less than 30 characters");
        }
    }
}
