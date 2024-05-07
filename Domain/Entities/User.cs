using Microsoft.AspNetCore.Identity;
using SocialApp.Domain.Entities.Comments;
using SocialApp.Domain.Entities.Posts;
using SocialApp.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SocialApp.Domain.Entities
{
    public class User:IdentityUser
    {
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; } = null!;
        [Required]
        [MaxLength(30)]
        public string LastName { get; set; } = null!;
        public string ?ImageUrl { get; set; }
        public UserStatus Status { get; set; } = UserStatus.Active;
        [JsonIgnore]
        public ICollection<Post> Posts { get; set; }
        [JsonIgnore]
        public ICollection<Comment> Comments{ get; set; }
    }
}
