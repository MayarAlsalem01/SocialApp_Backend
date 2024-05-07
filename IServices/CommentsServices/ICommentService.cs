using SocialApp.Domain.Entities.Comments;
using SocialApp.Dtos.CommentDtos;
using SocialApp.Dtos.PaginationDtos;

namespace SocialApp.IServices.CommentsServices
{
    /// <summary>
    /// Service interface defining operations related to comments.
    /// </summary>
    public interface ICommentService
    {
        /// <summary>
        /// Creates a new comment asynchronously.
        /// </summary>
        /// <param name="commentDto">The data transfer object containing information about the comment.</param>
        /// <returns>Returns the created comment DTO.</returns>
        /// <exception cref="UserNotFoundException">Thrown if the specified user does not exist.</exception>
        /// <exception cref="PostNotFoundException">Thrown if the specified post does not exist.</exception>
        Task<CommentResponseDto> CreateCommentAsync(string userId,CreateCommentDto commentDto);

        /// <summary>
        /// Deletes a comment asynchronously.
        /// </summary>
        /// <param name="commentDto">The data transfer object containing information about the comment to delete.</param>
        /// <returns>Returns the deleted comment DTO.</returns>
        /// <exception cref="CommentNotFoundException">Thrown if the specified comment does not exist.</exception>
        /// <exception cref="CommentForbiddenException">Thrown if the requesting user is not the owner of the comment.</exception>
        Task<CommentResponseDto> DeleteCommentAsync(DeleteCommentDto commentDto);

        /// <summary>
        /// Retrieves a comment by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the comment to retrieve.</param>
        /// <returns>Returns the comment DTO.</returns>
        /// <exception cref="CommentNotFoundException">Thrown if the specified comment does not exist.</exception>
        Task<CommentResponseDto> GetCommentByIdAsync(Guid id);

        /// <summary>
        /// Updates a comment asynchronously.
        /// </summary>
        /// <param name="commentDto">The data transfer object containing updated information about the comment.</param>
        /// <returns>Returns the updated comment DTO.</returns>
        /// <exception cref="UserNotFoundException">Thrown if the specified user does not exist.</exception>
        /// <exception cref="PostNotFoundException">Thrown if the specified post does not exist.</exception>
        /// <exception cref="CommentNotFoundException">Thrown if the specified comment does not exist.</exception>
        /// <exception cref="CommentForbiddenException">Thrown if the requesting user is not the owner of the comment.</exception>
        Task<CommentResponseDto> UpdateCommentAsync(string userId,UpdateCommentDto commentDto);

        Task<PaginationResponseDto<CommentResponseDto>> CommentsPaginationAsync(Guid postId, int pageSize,int page);
    }

}
