using AutoMapper;
using Jobsity.Chat.Application.Interfaces;
using Jobsity.Chat.Application.ViewModels;
using Jobsity.Chat.Application.ViewModels.Base;
using Jobsity.Chat.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Jobsity.Chat.Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserAppService(
            UserManager<User> userManager, 
            SignInManager<User> signInManager, 
            IConfiguration configuration,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<BaseResponse<IdentityResult>> Create(CreateUserViewModel user)
        {
            try
            {
                var serviceResult = await _userManager.CreateAsync(new User(user.Username, user.Name), user.Password);
                return new BaseResponse<IdentityResult>(serviceResult.Succeeded ? 
                    "User created with success, please login to access the chat!" : serviceResult.Errors.FirstOrDefault().Description, serviceResult, serviceResult.Succeeded);
            }
            catch (Exception ex)
            {
                return new BaseResponse<IdentityResult>(ex);
            }
            
        }

        public async Task<BaseResponse<UserViewModel>> GetCurrentUser(ClaimsPrincipal user)
        {
            try
            {
                var username = user.Identity.Name;
                var currentUser = await _userManager.FindByNameAsync(username);
                return new BaseResponse<UserViewModel>(_mapper.Map<UserViewModel>(currentUser));
            }
            catch (Exception ex)
            {
                return new BaseResponse<UserViewModel>(ex);
            }
            
        }

        public async Task<BaseResponse<LoginResponseViewModel>> Login(UserLoginViewModel userLoginViewModel)
        {
            try
            {
                var loginResult = await _signInManager.PasswordSignInAsync(userLoginViewModel.Username, userLoginViewModel.Password, false, false);
                if (loginResult.Succeeded)
                {
                    var tokenResponse = BuildToken(userLoginViewModel);
                    return new BaseResponse<LoginResponseViewModel>(tokenResponse);
                }
                else
                {
                    throw new Exception("Invalid Login");
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<LoginResponseViewModel>(ex);
            }
            
        }

        private LoginResponseViewModel BuildToken(UserLoginViewModel userLoginViewModel)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userLoginViewModel.Username),
                new Claim("Id", userLoginViewModel.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddHours(1);
            
            JwtSecurityToken token = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            return new LoginResponseViewModel(new JwtSecurityTokenHandler().WriteToken(token), expiration);
        }
    }
}
