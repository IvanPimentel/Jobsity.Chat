using AutoMapper;
using Jobsity.Chat.Application.Interfaces;
using Jobsity.Chat.Application.Services.Base;
using Jobsity.Chat.Application.ViewModels.Base;
using Jobsity.Chat.Application.ViewModels.ChatRoom;
using Jobsity.Chat.Application.ViewModels.ChatRoom.SignalR;
using Jobsity.Chat.Domain.Interfaces.Services;
using Jobsity.Chat.Domain.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Jobsity.Chat.Application.Services
{
    public class ChatRoomAppService : BaseAppService<ChatRoomViewModel, ChatRoom, IChatRoomService>, IChatRoomAppService
    {
        private readonly IHubContext<ChatRoomHub> _streaming;

        public ChatRoomAppService(
            IChatRoomService service, 
            IMapper mapper, 
            IHubContext<ChatRoomHub> streaming) : base(service, mapper)
        {
            _streaming = streaming;
        }

        public new async Task<BaseResponse<ChatRoomViewModel>> Create(ChatRoomViewModel model)
        {
            try
            {
                var domainModel = _mapper.Map<ChatRoom>(model);
                var serviceResult = await _service.Create(domainModel);

                var result = _mapper.Map<ChatRoomViewModel>(serviceResult);
                await _streaming.Clients.All.SendAsync("NewChatRoom", result);

                return new BaseResponse<ChatRoomViewModel>(result);
            }
            catch (Exception ex)
            {
                return new BaseResponse<ChatRoomViewModel>(ex);
            }
        }
    }
}
