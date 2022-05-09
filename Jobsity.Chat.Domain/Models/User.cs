using Jobsity.Chat.Core.Model;
using System.Collections.Generic;

namespace Jobsity.Chat.Domain.Models
{
    public class User : IdentityUser
    {
        public string Name { get; private set; }

        public virtual List<ChatRoomMessage> ChatRoomMessages { get; set; }


        public User(string userName, string name) : base(userName)
        {
            Name = name;
        }
    }
}
