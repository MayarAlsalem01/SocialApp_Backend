using SocialApp.Domain.Entities.Posts;
using SocialApp.Dtos.PaginationDtos;
using SocialApp.Dtos.PostDtos;

namespace SocialApp.IServices.PostsServices
{
    /// <summary>
    /// Service interface for managing posts.
    /// </summary>
    public interface IPostServices
    {
        /// <summary>
        /// Creates a new post asynchronously.
        /// </summary>
        /// <param name="dto">Data transfer object containing information for creating the post.</param>
        /// <param name="userId">The ID of the user creating the post.</param>
        /// <returns>The created post response DTO.</returns>
        /// <exception cref="UserNotFoundException">Thrown when the user specified by <paramref name="userId"/> is not found.</exception>
        Task<PostResponseDto> CreatePostAsync(CreatePostDto dto, string userId, string image);

        /// <summary>
        /// Deletes a post asynchronously.
        /// </summary>
        /// <param name="postId">The ID of the post to delete.</param>
        /// <param name="userId">The ID of the user requesting the deletion.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        /// <exception cref="UserNotFoundException">Thrown when the user specified by <paramref name="userId"/> is not found.</exception>
        /// <exception cref="PostNotFoundException">Thrown when the post specified by <paramref name="postId"/> is not found.</exception>
        /// <exception cref="PostUnauthorizedException">Thrown when the user specified by <paramref name="userId"/> is not authorized to delete the post.</exception>
        Task DeletePost(Guid postId, string userId);

        /// <summary>
        /// Retrieves a post by its ID asynchronously.
        /// </summary>
        /// <param name="postId">The ID of the post to retrieve.</param>
        /// <returns>The post response DTO.</returns>
        /// <exception cref="PostNotFoundException">Thrown when the post specified by <paramref name="postId"/> is not found.</exception>
        Task<PostResponseDto> GetPostByIdAsync(Guid postId);

        /// <summary>
        /// Updates a post asynchronously.
        /// </summary>
        /// <param name="dto">Data transfer object containing information for updating the post.</param>
        /// <param name="userId">The ID of the user requesting the update.</param>
        /// <param name="postId">The ID of the post to update.</param>
        /// <returns>The updated post response DTO.</returns>
        /// <exception cref="UserNotFoundException">Thrown when the user specified by <paramref name="userId"/> is not found.</exception>
        /// <exception cref="PostNotFoundException">Thrown when the post specified by <paramref name="postId"/> is not found.</exception>
        /// <exception cref="PostUnauthorizedException">Thrown when the user specified by <paramref name="userId"/> is not authorized to update the post.</exception>
        Task<PostResponseDto> UpdatePostsAsync(UpdatePostDto dto, string userId, Guid postId);

        PaginationResponseDto<PostResponseDto> PostPaginationAsync(int pageSize, int page);
    }
}
