using System.ComponentModel.DataAnnotations;

namespace Jobsity.Chat.Application.ViewModels
{
    public class UserViewModel
    {
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Username { get; set; }

        public string Email { get; set; }
        
        [Required]
        public string Name { get; set; }
    }
}
