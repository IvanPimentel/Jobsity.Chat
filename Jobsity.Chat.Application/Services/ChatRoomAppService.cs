﻿using AutoMapper;
using Jobsity.Chat.Application.Interfaces;
using Jobsity.Chat.Application.Services.Base;
using Jobsity.Chat.Application.ViewModels.ChatRoom;
using Jobsity.Chat.Domain.Interfaces.Services;
using Jobsity.Chat.Domain.Models;

namespace Jobsity.Chat.Application.Services
{
    public class ChatRoomAppService : BaseAppService<ChatRoomViewModel, ChatRoom, IChatRoomService>, IChatRoomAppService
    {
        public ChatRoomAppService(IChatRoomService service, IMapper mapper) : base(service, mapper)
        {
        }
    }
}