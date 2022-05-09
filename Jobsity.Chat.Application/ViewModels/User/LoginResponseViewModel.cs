using System;

namespace Jobsity.Chat.Application.ViewModels
{
    public class LoginResponseViewModel
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }

        public LoginResponseViewModel(string token, DateTime expiration)
        {
            Token = token;
            Expiration = expiration;
        }
    }
}
