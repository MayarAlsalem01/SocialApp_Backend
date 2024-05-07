using System.ComponentModel.DataAnnotations;

namespace SocialApp.Dtos.UserDto
{
    sealed public class LoginUserDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
