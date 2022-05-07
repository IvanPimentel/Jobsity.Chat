using Jobsity.Chat.Application.Interfaces;
using Jobsity.Chat.Application.ViewModels;
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

        public async Task<IdentityResult> Create([FromBody] UserViewModel user)
        {
            return await _userAppService.Create(user);
        }

        public async Task<UserViewModel> GetCurrentUser(UserViewModel user)
        {
            return await _userAppService.GetCurrentUser(user);
        }

        public async Task<LoginResponseViewModel> Login(LoginViewModel loginViewModel)
        {
            return await _userAppService.Login(loginViewModel);
        }
    }
}
