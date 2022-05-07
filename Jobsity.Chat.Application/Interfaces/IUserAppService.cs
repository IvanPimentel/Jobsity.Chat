using Jobsity.Chat.Application.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Jobsity.Chat.Application.Interfaces
{
    public interface IUserAppService
    {
        Task<IdentityResult> Create(CreateUserViewModel user);
        Task<LoginResponseViewModel> Login(UserLoginViewModel loginViewModel);
        Task<UserViewModel> GetCurrentUser(ClaimsPrincipal user);
    }
}
