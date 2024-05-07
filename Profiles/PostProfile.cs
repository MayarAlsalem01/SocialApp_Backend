using AutoMapper;
using SocialApp.Domain.Entities.Posts;
using SocialApp.Dtos.PostDtos;
using SocialApp.Dtos.UserDto;

namespace SocialApp.Profiles
{
    sealed public class PostProfile:Profile
    {
        public PostProfile()
        {
            CreateMap<CreatePostDto, Post>();
            CreateMap<Post, PostResponseDto>().ForMember(des=>des.CommentsNumber,act=>act.MapFrom(src=>src.Comments.Count));
                
            CreateMap<UpdatePostDto, Post>();
            
        }
    }
}
