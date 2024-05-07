using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialApp.Dtos.Response;
using SocialApp.Dtos.UserDto;
using SocialApp.IServices;
using SocialApp.IServices.UserServices;
using SocialApp.Middleware.Exceptions;

namespace SocialApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize()]
  
    public class UserController : ControllerBase
    {

        private readonly IUserServices _userServices;
        
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
           
        }
        [HttpGet("get-current-user")]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(ExceptionResponse))]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ExceptionResponse))]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(UserResponseDto))]
        public async Task<IActionResult> GetCrruentUser()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var user =await _userServices.GetUserByIdAsync(userId);
            var response = new ApiResponse<UserResponseDto>(
                                                      statusCode: 200,
                                                      message: "retrieve user success",
                                                      response: user);
            return Ok(response);
        }
        [HttpPost("add-user-to-role")]
        [ProducesResponseType(statusCode:StatusCodes.Status404NotFound,type:typeof(ExceptionResponse))]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ExceptionResponse))]
        [ProducesResponseType(statusCode: StatusCodes.Status201Created, type: typeof(UserResponseDto))]
        public async Task<IActionResult> AddUserToRole(string userId,string roleName)
        {
            var user = await _userServices.AddUserToRoleAsync(roleName,userId);
            var response = new ApiResponse<UserResponseDto>(
                                                      statusCode: 201,
                                                      message: "Add role to user success",
                                                      response: user);
            return Ok(response);
        }
        [HttpPut("{userId}/profile")]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(ExceptionResponse))]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ExceptionResponse))]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(UserResponseDto))]
        public async Task<IActionResult> EditProfile(EditUserProfileDto dto,[FromRoute]Guid userId)
        {
            var user = await _userServices.EditUserProfileAsync(dto);
            var response = new ApiResponse<UserResponseDto>(
                                                       statusCode: 200,
                                                       message: "update user success",
                                                       response: user);
            return Ok(response);
        }
    }
}
