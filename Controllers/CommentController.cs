using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialApp.Dtos.CommentDtos;
using SocialApp.Dtos.PaginationDtos;
using SocialApp.Dtos.Response;
using SocialApp.IServices.CommentsServices;
using SocialApp.Middleware.Exceptions;

namespace SocialApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentServices;
         
        public CommentController(ICommentService commentServices)
        {
            _commentServices = commentServices;
        }
        [HttpGet("commentId")]
       [ProducesResponseType(typeof(ApiResponse<CommentResponseDto>),200)]
      

        public async Task<IActionResult> Get(Guid commentId)
        {
            var comment =await _commentServices.GetCommentByIdAsync(commentId);
            if (comment == null) return NotFound();
            var response = new ApiResponse<CommentResponseDto>(
                                                                statusCode: 200,
                                                                message: "retrive data sucsess",
                                                                response: comment);
            return Ok(response);
        }
        [HttpGet("pagination")]
        [ProducesResponseType(typeof(ApiResponse<PaginationResponseDto<CommentResponseDto>>), 200)]
      

        public async Task<IActionResult> GetCommentsOfPostPagination(Guid postId, int page, int pageSize)
        {
            var comments = await _commentServices.CommentsPaginationAsync(postId, pageSize, page);
            var response = new ApiResponse<PaginationResponseDto<CommentResponseDto>>(
                                                               statusCode: 200,
                                                               message: "retrive data sucsess",
                                                               response: comments);
            return Ok(response);
        }
        [HttpPost("create")]
        [ProducesResponseType(typeof(ApiResponse<CommentResponseDto>), 200)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(ExceptionResponse))]

        public async Task<IActionResult>CreateComment(CreateCommentDto dto)
        {
            var userId = GetUserId();        
            var comment = await _commentServices.CreateCommentAsync(userId,dto);
            var response = new ApiResponse<CommentResponseDto>(
                                                               statusCode: 201,
                                                               message: "create comment sucsess",
                                                               response: comment);
            return Ok(response);

        }
        [HttpPut("update")]
        [ProducesResponseType(typeof(ApiResponse<CommentResponseDto>), 200)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(ExceptionResponse))]
        [ProducesResponseType(statusCode: StatusCodes.Status403Forbidden, type: typeof(ExceptionResponse))]
        public async Task<IActionResult>UpdateComment(UpdateCommentDto dto)
        {
            var userId= GetUserId();
            var updatedComment= await _commentServices.UpdateCommentAsync(userId,dto);
            var response = new ApiResponse<CommentResponseDto>(
                                                             statusCode: 200,
                                                             message: "update comment sucsess",
                                                             response: updatedComment);
            return Ok(response); 
        }

        [HttpDelete("commentId/delete")]
        [ProducesResponseType(typeof(ApiResponse<CommentResponseDto>), 200)]
        [ProducesResponseType(statusCode: StatusCodes.Status403Forbidden, type: typeof(ExceptionResponse))]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(ExceptionResponse))]

        public async Task<IActionResult> Delete(Guid commentId)
        {
            var userId=GetUserId();
            var deletedComment = await _commentServices.DeleteCommentAsync(new DeleteCommentDto() 
            { CommentId= commentId, UserId=userId});
            var response = new ApiResponse<CommentResponseDto>(
                                                            statusCode: 200,
                                                            message: "update comment sucsess",
                                                            response: deletedComment);
            return Ok(response);
        }
        private string GetUserId()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            return userId;
        }

    }
}
