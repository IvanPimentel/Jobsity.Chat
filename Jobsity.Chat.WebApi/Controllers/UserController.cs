using Jobsity.Chat.Application.Interfaces;
using Jobsity.Chat.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Jobsity.Chat.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserAppService _userAppService;

        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        public async Task<IdentityResult> Create([FromBody] CreateUserViewModel user)
        {
            return await _userAppService.Create(user);
        }

        [Authorize]
        public async Task<UserViewModel> GetCurrentUser()
        {
            return await _userAppService.GetCurrentUser(User);
        }

        public async Task<LoginResponseViewModel> Login(UserLoginViewModel userLoginViewModel)
        {
            return await _userAppService.Login(userLoginViewModel);
        }
    }
}
