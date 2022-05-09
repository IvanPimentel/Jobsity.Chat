using AutoMapper;
using Jobsity.Chat.Application.ViewModels;
using Jobsity.Chat.Application.ViewModels.ChatRoom;
using Jobsity.Chat.Domain.Models;

namespace Jobsity.Chat.Application.Mapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<UserViewModel, User>()
                .ConvertUsing(v => new User(v.Username, v.Name));

            CreateMap<ChatRoomViewModel, ChatRoom>()
                .ConstructUsing(v => new ChatRoom(v.Name));
        }
    }
}
