using Jobsity.Chat.Data.Context;
using Jobsity.Chat.Data.Repository.Base;
using Jobsity.Chat.Domain.Interfaces.Repositories;
using Jobsity.Chat.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobsity.Chat.Data.Repository
{
    public class ChatRoomMessageRepository : BaseRepository<ChatRoomMessage>, IChatRoomMessageRepository
    {
        public ChatRoomMessageRepository(ChatContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ChatRoomMessage>> GetByChatRoomId(Guid chatRoomId)
        {
            return await _context.ChatRoomsMessages.Where(c => c.ChatRoomId == chatRoomId).ToListAsync();
        }
    }
}
