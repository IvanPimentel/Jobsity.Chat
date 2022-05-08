using AutoMapper;
using Jobsity.Chat.Application.ViewModels;
using Jobsity.Chat.Domain.Models;

namespace Jobsity.Chat.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<User, UserViewModel>();
        }
    }
}
