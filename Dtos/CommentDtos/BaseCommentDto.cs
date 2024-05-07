using System.ComponentModel.DataAnnotations;

namespace SocialApp.Dtos.CommentDtos
{
    public abstract class BaseCommentDto
    {
        public Guid PostId { get; set; }
        public string CommentText { get; set; } = null!;

    }
}
