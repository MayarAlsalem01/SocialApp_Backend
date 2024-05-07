using SocialApp.Domain.Enums;

namespace SocialApp.Dtos.UserDto
{
    sealed public class AuthDto
    {

        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
      
        public string LastName { get; set; } = null!;
        public string Token { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = null!;
        public UserStatus Status { get; set; }
        
    }
}
