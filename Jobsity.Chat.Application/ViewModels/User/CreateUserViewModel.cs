using System.ComponentModel.DataAnnotations;

namespace Jobsity.Chat.Application.ViewModels
{
    public class CreateUserViewModel
    {
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Username { get; set; }

        [Required]
        public string Name { get; set; }

        public string Password { get; set; }
    }
}
