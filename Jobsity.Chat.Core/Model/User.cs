using Microsoft.AspNetCore.Identity;
using System;

namespace Jobsity.Chat.Core.Model
{
    public abstract class IdentityUser : IdentityUser<Guid>
    {
        protected IdentityUser(string userName) : base(userName)
        {
        }
    }
}
