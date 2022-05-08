using Jobsity.Chat.Application.Interfaces;
using Jobsity.Chat.Application.ViewModels;
using Jobsity.Chat.Application.ViewModels.Base;
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

        [HttpPost]
        public async Task<BaseResponse<IdentityResult>> Create([FromBody] CreateUserViewModel user)
        {
            return await _userAppService.Create(user);
        }

        [HttpGet][Authorize]
        public async Task<BaseResponse<UserViewModel>> GetCurrentUser()
        {
            return await _userAppService.GetCurrentUser(User);
        }

        [HttpPost("Login")]
        public async Task<BaseResponse<LoginResponseViewModel>> Login(UserLoginViewModel userLoginViewModel)
        {
            return await _userAppService.Login(userLoginViewModel);
        }
    }
}
