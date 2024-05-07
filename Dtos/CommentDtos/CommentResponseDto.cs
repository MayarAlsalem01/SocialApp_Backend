using SocialApp.Domain.Entities;
using SocialApp.Dtos.UserDto;

namespace SocialApp.Dtos.CommentDtos
{
    public class CommentResponseDto:BaseCommentDto
    {
        public  string CommentText { get; set; } = null!;
        public Guid Id { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
        public UserResponseDto User { get; set; }
    }
}
