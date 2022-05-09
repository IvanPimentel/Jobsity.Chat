using Jobsity.Chat.Application.Interfaces;
using Jobsity.Chat.Application.ViewModels.Base;
using Jobsity.Chat.Application.ViewModels.ChatRoom;
using Jobsity.Chat.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jobsity.Chat.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatRoomMessageController : BaseController<ChatRoomMessageViewModel, IChatRoomMessageAppService>
    {
        public ChatRoomMessageController(IChatRoomMessageAppService appService) : base(appService)
        {
        }

        [HttpGet("GetByChatRoomId/{chatRoomId}")]
        public async Task<BaseResponse<IEnumerable<ChatRoomMessageViewModel>>> GetByChatRoomId(Guid chatRoomId)
        {
            return await _appService.GetByChatRoomId(chatRoomId);
        }
    }
}
