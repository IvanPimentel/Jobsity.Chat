using Jobsity.Chat.Application.Interfaces;
using Jobsity.Chat.Application.ViewModels.ChatRoom;
using Jobsity.Chat.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Jobsity.Chat.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatRoomMessageController : BaseController<ChatRoomMessageViewModel, IChatRoomMessageAppService>
    {
        public ChatRoomMessageController(IChatRoomMessageAppService appService) : base(appService)
        {
        }
    }
}
