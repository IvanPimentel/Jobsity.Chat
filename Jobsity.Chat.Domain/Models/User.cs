using Jobsity.Chat.Core.Model;

namespace Jobsity.Chat.Domain.Models
{
    public class User : IdentityUser
    {
        public string Name { get; private set; }

        public User(string userName, string name) : base(userName)
        {
            Name = name;
        }
    }
}
