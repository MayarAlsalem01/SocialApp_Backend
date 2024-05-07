using System.ComponentModel.DataAnnotations;

namespace SocialApp.Dtos.UserDto
{
    public sealed class EditUserProfileDto
    {
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; } = null!;
        [Required]
        [MaxLength(30)]
        public string LastName { get; set; } = null!;
        [Required]
        public string Id { get; set; } = null!;
        [Required]
        public string ImageUrl { get; set; } = null!;
    }
}
