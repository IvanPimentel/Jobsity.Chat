using Jobsity.Chat.Domain.Interfaces.Repositories;
using Jobsity.Chat.Domain.Interfaces.Services;
using Jobsity.Chat.Domain.Models;
using Jobsity.Chat.Domain.Services.Base;

namespace Jobsity.Chat.Domain.Services
{
    public class ChatRoomService : BaseService<ChatRoom, IChatRoomRepository>, IChatRoomService
    {
        public ChatRoomService(IChatRoomRepository repository) : base(repository)
        {
        }
    }
}
