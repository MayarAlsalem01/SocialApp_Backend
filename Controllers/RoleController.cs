using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialApp.Domain.Entities;
using SocialApp.Dtos.Response;
using SocialApp.IServices;

namespace SocialApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="admin")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpGet()]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(ApiResponse<Role>))]
        public async Task<IActionResult> GetRoleByNameAsync([FromQuery]string roleName)
        {
            var role = await _roleService.GetRoleByNameAsync(roleName);
            if(role == null)return NotFound(role);
            var response = new ApiResponse<Role>(
                                                         statusCode: 200,
                                                         message: "retrieve role success",
                                                         response: role);
            return Ok(response);
        }
        [HttpGet("roles")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(ApiResponse<IEnumerable<Role>>))]
        public async Task<IActionResult> GetAllRolesAsync()
        {
            var roles = await _roleService.GetAllRoles();
            var response = new ApiResponse<IEnumerable<Role>>(
                                                         statusCode: 200,
                                                         message: "retrieve role success",
                                                         response: roles);
            return Ok(response);
        }
        [HttpPost("create")]
        [ProducesResponseType(statusCode: StatusCodes.Status201Created, type: typeof(ApiResponse<Role>))]
        public async Task<IActionResult>CreateRoleAsync([FromBody] string roleName)
        {
            var role = await _roleService.CreateNewRole(roleName);
            var response = new ApiResponse<Role>(
                                                       statusCode: 201,
                                                       message: "Create role success",
                                                       response: role);
            return Ok(response);
        }
        [HttpPut("roleName/update")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(ApiResponse<Role>))]
        public async Task<IActionResult>UpdateRoleAsync(string roleName,[FromBody] string newRoleName)
        {
            var role = await _roleService.UpdateRoleAsync(roleName,newRoleName);
            var response = new ApiResponse<Role>(
                                                       statusCode: 200,
                                                       message: "update role success",
                                                       response: role);
            return Ok(response);
        }
        [HttpDelete("delete")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(ApiResponse<Role>))]
        public async Task<IActionResult> DeleteRoleAsync([FromQuery]string roleName)
        {
            var role=await _roleService.Delete(roleName);
            var response = new ApiResponse<Role>(
                                                       statusCode: 200,
                                                       message: "update role success",
                                                       response: role);
            return Ok(response);
        }


    }
}
