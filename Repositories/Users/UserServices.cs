using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SocialApp.Domain.Entities;
using SocialApp.Domain.Exceptions.Common;
using SocialApp.Domain.Exceptions.RoleExceptions;
using SocialApp.Domain.Exceptions.UserExceptions;
using SocialApp.Dtos.UserDto;
using SocialApp.IServices;
using SocialApp.IServices.UserServices;

namespace SocialApp.Repositories.Users
{
    sealed public class UserServices : IUserServices
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IRoleService _roleServices;
       
        public UserServices(UserManager<User> userManager, IMapper mapper, IRoleService roleServices)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleServices = roleServices;
        }

        public async Task<UserResponseDto> GetUserByIdAsync(string userId)
        {
            var user= await _userManager.FindByIdAsync(userId);
            
            if (user == null) return null;
            var roles =await _userManager.GetRolesAsync(user);
            var response = _mapper.Map<UserResponseDto>(user);
            response.Roles = roles;
            return response;
        }
        public async Task<UserResponseDto> AddUserToRoleAsync(string roleName, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            await ValidateUserAsync(userId);
            var role = await _roleServices.GetRoleByNameAsync(roleName);
            if (role is null) throw new RoleNotFoundException($"the role : {roleName.ToUpper()} didn't find ");
            var result = await _userManager.AddToRoleAsync(user, role.Name);
            if (!result.Succeeded) throw new UserBadRequestException("user  couldn't add to role ", result.Errors.ToDictionary(res => res.Code, message => message.Description));
            var response = _mapper.Map<UserResponseDto>(user);
            response.Roles = await _userManager.GetRolesAsync(user);
            return response;
        }

        public async Task<UserResponseDto> EditUserProfileAsync(EditUserProfileDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.Id);
            await ValidateUserAsync(dto.Id);
            _mapper.Map(dto, user);
           var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) throw new UserBadRequestException("bad request", result.Errors.ToDictionary(res=>res.Code,des=>des.Description));
            var roles = await  _userManager.GetRolesAsync(user);
            var response= _mapper.Map<UserResponseDto>(user);
            response.Roles = roles;
            return _mapper.Map<UserResponseDto>(user);

        }
        private async Task ValidateUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user is null)
            throw new UserNotFoundException($"user with id : {userId } not found ");
        }
    }
}
