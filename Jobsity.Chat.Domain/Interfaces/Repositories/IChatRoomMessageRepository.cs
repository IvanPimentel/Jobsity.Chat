using Jobsity.Chat.Domain.Interfaces.Repositories.Base;
using Jobsity.Chat.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jobsity.Chat.Domain.Interfaces.Repositories
{
    public interface IChatRoomMessageRepository : IBaseRepository<ChatRoomMessage>
    {
        Task<IEnumerable<ChatRoomMessage>> GetByChatRoomId(Guid chatRoomId);
    }
}
