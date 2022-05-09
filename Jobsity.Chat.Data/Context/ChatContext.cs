using Jobsity.Chat.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Jobsity.Chat.Data.Context
{
    public class ChatContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        //public DbSet<ChatRoom> ChatRooms { get; set; }

        public ChatContext(DbContextOptions options) : base(options)
        {


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ChatContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
