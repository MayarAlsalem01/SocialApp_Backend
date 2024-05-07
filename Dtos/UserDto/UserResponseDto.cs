using System.ComponentModel.DataAnnotations;

namespace SocialApp.Dtos.UserDto
{
    sealed public class UserResponseDto
    {
        [Required]
        public string Id { get; set; } = null!;
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public IEnumerable<string> Roles { get; set; } = null!;
    }
}
