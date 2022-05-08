using Jobsity.Chat.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Jobsity.Chat.Data.Context
{
    public class ChatContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public ChatContext(DbContextOptions options) : base(options)
        {
        }
    }
}
