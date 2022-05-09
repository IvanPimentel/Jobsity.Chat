using Jobsity.Chat.Domain.Interfaces.Repositories;
using Jobsity.Chat.Domain.Interfaces.Services;
using Jobsity.Chat.Domain.Models;
using Jobsity.Chat.Domain.Services.Base;

namespace Jobsity.Chat.Domain.Services
{
    public class ChatRoomMessageService : BaseService<ChatRoomMessage, IChatRoomMessageRepository>, IChatRoomMessageService
    {
        public ChatRoomMessageService(IChatRoomMessageRepository repository) : base(repository)
        {
        }
    }
}
