using System.ComponentModel.DataAnnotations;

namespace Jobsity.Chat.Application.ViewModels
{
    public class UserLoginViewModel
    {
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
