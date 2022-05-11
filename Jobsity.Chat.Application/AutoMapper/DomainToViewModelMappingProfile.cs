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
            CreateMap<ChatRoomMessage, ChatRoomMessageViewModel>()
                .ForMember(v => v.Username, d => d.MapFrom(d => d.User.UserName))
                .ForMember(v => v.Name, d => d.MapFrom(d => d.User.Name));

        }
    }
}
