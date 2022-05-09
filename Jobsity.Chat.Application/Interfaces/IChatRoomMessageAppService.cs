using Jobsity.Chat.Application.Interfaces.Base;
using Jobsity.Chat.Application.ViewModels.Base;
using Jobsity.Chat.Application.ViewModels.ChatRoom;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jobsity.Chat.Application.Interfaces
{
    public interface IChatRoomMessageAppService : IBaseAppService<ChatRoomMessageViewModel>
    {
        Task<BaseResponse<IEnumerable<ChatRoomMessageViewModel>>> GetByChatRoomId(Guid chatRoomId);
    }
}
