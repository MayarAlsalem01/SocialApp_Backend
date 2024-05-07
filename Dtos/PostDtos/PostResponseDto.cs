using SocialApp.Domain.Entities;
using SocialApp.Dtos.CommentDtos;
using SocialApp.Dtos.UserDto;

namespace SocialApp.Dtos.PostDtos
{
     
    public class PostResponseDto
    {
        public Guid Id { get; set; }
     

        public string Content { get; set; }
        public string? PostImage { get; set; }
        public DateTime CreateAt { get; set; }
        public UserResponseDto User { get; set; }
        public int CommentsNumber { get; set; } = 0;
    }
   
}
