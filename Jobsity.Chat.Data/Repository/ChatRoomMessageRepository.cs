using Jobsity.Chat.Data.Context;
using Jobsity.Chat.Data.Repository.Base;
using Jobsity.Chat.Domain.Interfaces.Repositories;
using Jobsity.Chat.Domain.Models;

namespace Jobsity.Chat.Data.Repository
{
    public class ChatRoomMessageRepository : BaseRepository<ChatRoomMessage>, IChatRoomMessageRepository
    {
        public ChatRoomMessageRepository(ChatContext context) : base(context)
        {
        }
    }
}
