using AutoMapper;
using Jobsity.Chat.Application.Interfaces;
using Jobsity.Chat.Application.ViewModels;
using Jobsity.Chat.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
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

        public async Task<IdentityResult> Create(CreateUserViewModel user)
        {
            return await _userManager.CreateAsync(new User(user.Username, user.Name), user.Password);
        }

        public async Task<UserViewModel> GetCurrentUser(ClaimsPrincipal user)
        {
            var currentUser = await _userManager.GetUserAsync(user);
            return _mapper.Map<UserViewModel>(currentUser);
        }

        public async Task<LoginResponseViewModel> Login(UserLoginViewModel userLoginViewModel)
        {
            var loginResult = await _signInManager.PasswordSignInAsync(userLoginViewModel.Username, userLoginViewModel.Password, false, false);
            if (loginResult.Succeeded)
            {
                return BuildToken(userLoginViewModel);
            }
            else
            {
                throw new Exception("Invalid Login");
            }
        }

        private LoginResponseViewModel BuildToken(UserLoginViewModel userLoginViewModel)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userLoginViewModel.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["OAuth:Secret"]));
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
