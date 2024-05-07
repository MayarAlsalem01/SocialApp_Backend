
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialApp.Dtos.PaginationDtos;
using SocialApp.Dtos.PostDtos;
using SocialApp.Dtos.Response;
using SocialApp.IServices.CommentsServices;
using SocialApp.IServices.ImageServices;
using SocialApp.IServices.PostsServices;
using SocialApp.Middleware.Exceptions;

namespace SocialApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
       
        private readonly IPostServices _postServices;
        private readonly IImageServices _ImageServices;

        public PostController(IPostServices postServices, IImageServices imageServices)
        {

            _postServices = postServices;
            _ImageServices = imageServices;
        }
        [HttpGet("id")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<PostResponseDto>), 200)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(ExceptionResponse))]
       
        public async Task<IActionResult> GetPostById([FromQuery] Guid id)
        {
            var post = await _postServices.GetPostByIdAsync(id);
            var response = new ApiResponse<PostResponseDto>(
                                                            statusCode: 200,
                                                            message: "retrieve data success",
                                                            response:post);
            return Ok(response);
        }
        [HttpGet()]
        [ProducesResponseType(typeof(ApiResponse<PaginationResponseDto<PostResponseDto>>), 200)]
        [AllowAnonymous]
        public  IActionResult GetAll(int pageNumber, int pageSize)
        {
            var posts = _postServices.PostPaginationAsync(pageSize, pageNumber);
            var response = new ApiResponse<PaginationResponseDto<PostResponseDto>>(
                                                           statusCode: 200,
                                                           message: "retrieve data success",
                                                           response: posts);
            return Ok(response);
        }
       
        [HttpPost("create")]
        [ProducesResponseType(typeof(ApiResponse<PostResponseDto>), 201)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(ExceptionResponse))]
     
        public async Task<IActionResult> CreatePostAsync([FromForm]CreatePostDto dto)
        {
            
            if(!ModelState.IsValid) return BadRequest(dto);
            string imageBase64 = null;
            if(dto.File is not null)
            {
                //to check if the file is an image
                if(! isImage(dto.File)) return BadRequest(new ApiException(400, "only image accepted ", new Dictionary<string, string>()));
                //check if the image is sensetive or not 
                var postImage = new byte[dto.File.Length];
                dto.File.OpenReadStream().Read(postImage, 0, postImage.Length);
                var isNsfw = await _ImageServices.IsNsfwImage(postImage);
                if (isNsfw) return BadRequest(new ApiException(400, "you can't add sensitve image ", new Dictionary<string, string>()));
                // to convert the image to base64 arry
                imageBase64 = Convert.ToBase64String(postImage);
            }
            
           
            var userId= User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var post = await _postServices.CreatePostAsync(dto, userId,imageBase64);
            var response = new ApiResponse<PostResponseDto>(
                                                            statusCode: 201,
                                                            message: "create post success",
                                                            response: post);
            return Ok(response);
        }
        [HttpPut("postId/update")]
        [ProducesResponseType(typeof(ApiResponse<PostResponseDto>), 200)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(ExceptionResponse))]
        [ProducesResponseType(statusCode: StatusCodes.Status403Forbidden, type: typeof(ExceptionResponse))]
        public async Task<IActionResult> UpdatePostAsync([FromBody]UpdatePostDto dto,Guid postId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return NotFound();
           var post =await _postServices.UpdatePostsAsync(dto, userId, postId);
            var response = new ApiResponse<PostResponseDto>(
                                                           statusCode: 201,
                                                           message: "create post success",
                                                           response: post);
            return Ok(post);
        }
        [HttpDelete("postId/delete")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(ApiResponse<PostResponseDto>))]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(ExceptionResponse))]
        [ProducesResponseType(statusCode: StatusCodes.Status403Forbidden, type: typeof(ExceptionResponse))]
        public async Task<IActionResult> DeletePost([FromQuery]Guid postId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            await _postServices.DeletePost(postId,userId);
            var response = new ApiResponse<PostResponseDto>(
                                                         statusCode: 200,
                                                         message: "delete post success",
                                                         response: null);
            return Ok(response);
        }
      private bool isImage(IFormFile file)
        {
            return file.Length>0 && file.ContentType.StartsWith("image/") ;
        }
        
    }
}
