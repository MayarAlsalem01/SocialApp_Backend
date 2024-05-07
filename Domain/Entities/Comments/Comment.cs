using SocialApp.Domain.Entities.Posts;
using System.ComponentModel.DataAnnotations;

namespace SocialApp.Domain.Entities.Comments
{
    sealed public class Comment:BaseEntity
    {
        
        [Required]
        public string CommentText { get; set; } = null!;
        
        public string UserId { get; set; }=null!;
        public User User { get; set; } = null!;

        public Guid PostId { get; set; }
        public Post Post { get; set; } = null!;

    }
}
