using Jobsity.Chat.Domain.Interfaces.Repositories;
using Jobsity.Chat.Domain.Interfaces.Services;
using Jobsity.Chat.Domain.Models;
using Jobsity.Chat.Domain.Services.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jobsity.Chat.Domain.Services
{
    public class ChatRoomMessageService : BaseService<ChatRoomMessage, IChatRoomMessageRepository>, IChatRoomMessageService
    {
        public ChatRoomMessageService(IChatRoomMessageRepository repository) : base(repository)
        {
        }

        public async Task<IEnumerable<ChatRoomMessage>> GetByChatRoomId(Guid chatRoomId)
        {
            return await _repository.GetByChatRoomId(chatRoomId);
        }
    }
}
