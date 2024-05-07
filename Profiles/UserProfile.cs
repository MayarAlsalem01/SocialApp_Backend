using AutoMapper;
using SocialApp.Domain.Entities;
using SocialApp.Dtos.UserDto;

namespace SocialApp.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<User,AuthDto>();
            CreateMap<User, UserResponseDto>();
            CreateMap<CreateUserDto, User>();
            CreateMap<EditUserProfileDto, User>().ReverseMap();
        }
    }
}
