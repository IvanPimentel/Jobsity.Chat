using AutoMapper;
using Jobsity.Chat.Application.Interfaces;
using Jobsity.Chat.Application.Services.Base;
using Jobsity.Chat.Application.ViewModels.Base;
using Jobsity.Chat.Application.ViewModels.ChatRoom;
using Jobsity.Chat.Application.ViewModels.ChatRoom.SignalR;
using Jobsity.Chat.Domain.Interfaces.Services;
using Jobsity.Chat.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Jobsity.Chat.Application.Services
{
    public class ChatRoomMessageAppService : BaseAppService<ChatRoomMessageViewModel, ChatRoomMessage, IChatRoomMessageService>, IChatRoomMessageAppService
    {

        private readonly IHubContext<ChatRoomMessageHub> _streaming;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public ChatRoomMessageAppService(
            IChatRoomMessageService service,
            IMapper mapper,
            IHubContext<ChatRoomMessageHub> streaming,
            IHttpContextAccessor httpContextAccessor, 
            UserManager<User> userManager) : base(service, mapper)
        {
            _streaming = streaming;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public new async Task<BaseResponse<ChatRoomMessageViewModel>> Create(ChatRoomMessageViewModel model)
        {
            try
            {
                var userName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var user = await _userManager.FindByNameAsync(userName);
                model.UserId = user.Id;
                var domainModel = _mapper.Map<ChatRoomMessage>(model);
                var serviceResult = await _service.Create(domainModel);
                var result = _mapper.Map<ChatRoomMessageViewModel>(serviceResult);
                await _streaming.Clients.All.SendAsync("NewChatMessage", result);
                return new BaseResponse<ChatRoomMessageViewModel>(result);
            }
            catch (Exception ex)
            {
                return new BaseResponse<ChatRoomMessageViewModel>(ex);
            }
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
