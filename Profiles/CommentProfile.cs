using AutoMapper;
using SocialApp.Domain.Entities.Comments;
using SocialApp.Dtos.CommentDtos;

namespace SocialApp.Profiles
{
    public class CommentProfile:Profile
    {
        public CommentProfile()
        {
            CreateMap<CreateCommentDto, Comment>();
            CreateMap<Comment, CommentResponseDto>();
            CreateMap<UpdateCommentDto, Comment>();
        }
    }
}
