using Jobsity.Chat.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobsity.Chat.Data.EntityTypeConfigurations
{
    public class ChatRoomMessageConfiguration : IEntityTypeConfiguration<ChatRoomMessage>
    {
        public void Configure(EntityTypeBuilder<ChatRoomMessage> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Content)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(x => x.User)
                .WithMany(x => x.ChatRoomMessages)
                .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.ChatRoom)
                .WithMany(x => x.ChatRoomMessages)
                .HasForeignKey(x => x.ChatRoomId);

        }
    }
}
