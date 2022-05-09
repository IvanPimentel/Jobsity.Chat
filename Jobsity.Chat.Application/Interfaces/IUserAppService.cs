using Jobsity.Chat.Application.ViewModels;
using Jobsity.Chat.Application.ViewModels.Base;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Jobsity.Chat.Application.Interfaces
{
    public interface IUserAppService
    {
        Task<BaseResponse<IdentityResult>> Create(CreateUserViewModel user);
        Task<BaseResponse<LoginResponseViewModel>> Login(UserLoginViewModel loginViewModel);
        Task<BaseResponse<UserViewModel>> GetCurrentUser(ClaimsPrincipal user);
    }
}
