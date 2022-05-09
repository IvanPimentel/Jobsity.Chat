using Jobsity.Chat.Domain.Interfaces.Services.Base;
using Jobsity.Chat.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jobsity.Chat.Domain.Interfaces.Services
{
    public interface IChatRoomMessageService : IBaseService<ChatRoomMessage>
    {
        Task<IEnumerable<ChatRoomMessage>> GetByChatRoomId(Guid chatRoomId);
    }
}
