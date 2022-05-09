using Jobsity.Chat.Application.Interfaces;
using Jobsity.Chat.Application.ViewModels.ChatRoom;
using Jobsity.Chat.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Jobsity.Chat.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatRoomController : BaseController<ChatRoomViewModel, IChatRoomAppService>
    {
        public ChatRoomController(IChatRoomAppService appService) : base(appService)
        {
        }
    }
}
