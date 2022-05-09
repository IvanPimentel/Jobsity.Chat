using AutoMapper;
using Jobsity.Chat.Application.Interfaces;
using Jobsity.Chat.Application.Services.Base;
using Jobsity.Chat.Application.ViewModels.Base;
using Jobsity.Chat.Application.ViewModels.ChatRoom;
using Jobsity.Chat.Domain.Interfaces.Services;
using Jobsity.Chat.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jobsity.Chat.Application.Services
{
    public class ChatRoomMessageAppService : BaseAppService<ChatRoomMessageViewModel, ChatRoomMessage, IChatRoomMessageService>, IChatRoomMessageAppService
    {
        public ChatRoomMessageAppService(IChatRoomMessageService service, IMapper mapper) : base(service, mapper)
        {
        }

        public async Task<BaseResponse<IEnumerable<ChatRoomMessageViewModel>>> GetByChatRoomId(Guid chatRoomId)
        {
            try
            {
                var serviceResult = await _service.GetByChatRoomId(chatRoomId);
                return new BaseResponse<IEnumerable<ChatRoomMessageViewModel>>(_mapper.Map<IEnumerable<ChatRoomMessageViewModel>>(serviceResult));
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<ChatRoomMessageViewModel>>(ex);
            }
            
        }
    }
}
