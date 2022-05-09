using AutoMapper;
using Jobsity.Chat.Application.ViewModels;
using Jobsity.Chat.Application.ViewModels.ChatRoom;
using Jobsity.Chat.Domain.Models;

namespace Jobsity.Chat.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<ChatRoom, ChatRoomViewModel>();
            CreateMap<ChatRoomMessage, ChatRoomMessageViewModel>();

        }
    }
}
