using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialApp.Domain.Entities.Comments;
using SocialApp.Domain.Exceptions.CommentExceptions;
using SocialApp.Domain.Exceptions.PostExceptions;
using SocialApp.Domain.Exceptions.UserExceptions;
using SocialApp.Dtos.CommentDtos;
using SocialApp.Dtos.PaginationDtos;
using SocialApp.IRepository;
using SocialApp.IServices.CommentsServices;
using SocialApp.IServices.PostsServices;
using SocialApp.IServices.UserServices;
using System;

namespace SocialApp.Services.CommentsServices
{
    public class CommentServices : ICommentService
    {
        private readonly IGenericRepository<Comment> _repository;
        private readonly IMapper _mapper;
        private readonly IPostServices _postServices;
        private readonly IUserServices _userServices;

        public CommentServices(
            IGenericRepository<Comment> repository,
            IMapper mapper,
            IPostServices postServices,
            IUserServices userServices)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _postServices = postServices ?? throw new ArgumentNullException(nameof(postServices));
            _userServices = userServices ?? throw new ArgumentNullException(nameof(userServices));
        }

        public async Task<CommentResponseDto> CreateCommentAsync(string userId,CreateCommentDto commentDto)
        {
            await ValidateCommentAsync(userId, commentDto.PostId);
            var comment = _mapper.Map<Comment>(commentDto);
            comment.UserId=userId;
            await _repository.InserAsync(comment);
            await _repository.SaveChangesAsync();
            return _mapper.Map<CommentResponseDto>(comment);
        }

        public async Task<CommentResponseDto> DeleteCommentAsync(DeleteCommentDto commentDto)
        {
            var comment = await GetExistingCommentAsync(commentDto.CommentId);
            ValidateCommentOwnership(commentDto.UserId, comment.UserId);
            _repository.Delete(comment);
            await _repository.SaveChangesAsync();
            return _mapper.Map<CommentResponseDto>(comment);
        }

        public async Task<CommentResponseDto> GetCommentByIdAsync(Guid id)
        {
            var comment = await GetExistingCommentAsync(id);
            return _mapper.Map<CommentResponseDto>(comment);
        }

        public async Task<CommentResponseDto> UpdateCommentAsync(string userId,UpdateCommentDto commentDto)
        {
            var comment = await GetExistingCommentAsync(commentDto.CommentId);
            await ValidateCommentAsync(userId,commentDto.PostId);
            ValidateCommentOwnership(userId, comment.UserId);
            _mapper.Map(commentDto, comment);
            _repository.Update(comment);
            
            await _repository.SaveChangesAsync();
            return _mapper.Map<CommentResponseDto>(comment);
        }

        private async Task<Comment> GetExistingCommentAsync(Guid commentId)
        {
          
            var comment =await _repository.Include(x => x.User).FirstOrDefaultAsync(c=>c.Id==commentId);
            if (comment == null)
                throw new CommentNotFoundException($"Comment with ID '{commentId}' not found.");
            return comment;
        }

        private async Task ValidateCommentAsync(string userId,Guid postId)
        {
            await ValidateUserAsync(userId);
            await ValidatePostAsync(postId);
        }

        private async Task ValidateUserAsync(string userId)
        {
            if (await _userServices.GetUserByIdAsync(userId) == null)
                throw new UserNotFoundException($"User with ID '{userId}' not found.");
        }

        private async Task ValidatePostAsync(Guid postId)
        {
            if (await _postServices.GetPostByIdAsync(postId) == null)
                throw new PostNotFoundException($"Post with ID '{postId}' not found.");
        }

        private void ValidateCommentOwnership(string requestingUserId, string commentOwnerId)
        {
            if (requestingUserId != commentOwnerId)
                throw new CommentForbiddenException("Forbidden, invalid credentials.");
        }

        public  async Task<PaginationResponseDto<CommentResponseDto>> CommentsPaginationAsync(Guid postId,int pageSize, int page)
        {
            // if post not found the function will throw PostNotFoundExcepetion 
            await ValidatePostAsync(postId);
            var  comments = CommentsOfPost(postId);
            int totalRecords = comments.Count();
            var data= _repository.GetPagination(comments, page, pageSize);
            
            return new PaginationResponseDto<CommentResponseDto>(page,pageSize,totalRecords,_mapper.Map<List<CommentResponseDto>>(data));


        }
        private IEnumerable<Comment> CommentsOfPost(Guid postId )
        {
            return _repository.Include(comment=>comment.User).Where(comment => comment.PostId == postId);
        }
    }
}
