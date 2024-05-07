using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialApp.Domain.Entities;
using SocialApp.Dtos.Response;
using SocialApp.Dtos.UserDto;
using SocialApp.IdentityServices;
using SocialApp.IRepository;
using SocialApp.IServices;
using SocialApp.Middleware.Exceptions;

namespace SocialApp.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
       
       

        private readonly IIdendtityServices _userServices;

        public AuthController(IIdendtityServices userServices)
        {
            _userServices = userServices;
        }
        [HttpPost("rigster")]
        [ProducesResponseType(typeof(ApiResponse<AuthDto>), 200)]
        [ProducesResponseType(statusCode:StatusCodes.Status400BadRequest,type:typeof(ExceptionResponse))]

        public async Task<IActionResult> CreateUser(CreateUserDto dto)
        {
            var user = await _userServices.CreateAsync(dto);
            var response = new ApiResponse<AuthDto>() { statusCode = 200, message = "Register Sucessful", Response = user };
            return Ok(response);
        }
        /// <summary>
        /// signIn endpoint
        /// </summary>
        /// <returns></returns>
        /// <param name="dto"></param>
        [ProducesResponseType(typeof(ApiResponse<AuthDto>),200)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ExceptionResponse))]

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto dto)
        {
            var user = await _userServices.LognAsync(dto);
           
            var response = new ApiResponse<AuthDto>() { statusCode=200,message="Login Sucessful",Response=user};
            
          
            return Ok(response);
        }
    }
}
