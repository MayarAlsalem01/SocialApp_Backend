using System.ComponentModel.DataAnnotations;

namespace SocialApp.Dtos.CommentDtos
{
    public class DeleteCommentDto
    {
        [Required]
        public string UserId { get; set; } = null!;
        [Required]
        public Guid CommentId { get; set; }
    }
}
