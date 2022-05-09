using AutoMapper;
using Jobsity.Chat.Application.Interfaces;
using Jobsity.Chat.Application.Services.Base;
using Jobsity.Chat.Application.ViewModels.ChatRoom;
using Jobsity.Chat.Domain.Interfaces.Services;
using Jobsity.Chat.Domain.Models;

namespace Jobsity.Chat.Application.Services
{
    public class ChatRoomMessageAppService : BaseAppService<ChatRoomMessageViewModel, ChatRoomMessage, IChatRoomMessageService>, IChatRoomMessageAppService
    {
        public ChatRoomMessageAppService(IChatRoomMessageService service, IMapper mapper) : base(service, mapper)
        {
        }
    }
}
