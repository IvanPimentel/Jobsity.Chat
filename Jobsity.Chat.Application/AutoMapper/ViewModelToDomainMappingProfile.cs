using AutoMapper;
using Jobsity.Chat.Application.ViewModels;
using Jobsity.Chat.Domain.Models;

namespace Jobsity.Chat.Application.Mapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<UserViewModel, User>()
                .ConvertUsing(u => new User(u.Username, u.Name));
        }
    }
}
