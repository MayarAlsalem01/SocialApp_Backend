using SocialApp.Domain.Entities;
using SocialApp.Dtos.UserDto;

namespace SocialApp.IServices.UserServices
{
    public interface IUserServices
    {
        Task<UserResponseDto> GetUserByIdAsync(string userId);
        Task<UserResponseDto> AddUserToRoleAsync(string roleName, string userId);
        Task<UserResponseDto> EditUserProfileAsync(EditUserProfileDto dto);


    }
}
