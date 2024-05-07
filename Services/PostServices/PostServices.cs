using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialApp.Domain.DomainEvents.PostEvents;
using SocialApp.Domain.Entities.Posts;
using SocialApp.Domain.Exceptions.PostExceptions;
using SocialApp.Domain.Exceptions.UserExceptions;
using SocialApp.Dtos.PaginationDtos;
using SocialApp.Dtos.PostDtos;
using SocialApp.Dtos.UserDto;
using SocialApp.IRepository;
using SocialApp.IServices.PostsServices;
using SocialApp.IServices.UserServices;


namespace SocialApp.Services.PostServices
{
    public class PostServices : IPostServices
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Post> _genericRepository;
        private readonly IUserServices _userServices;
       
        public PostServices(IMapper mapper, IGenericRepository<Post> genericRepository, IUserServices userServices)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _genericRepository = genericRepository ?? throw new ArgumentNullException(nameof(genericRepository));
            _userServices = userServices ?? throw new ArgumentNullException(nameof(userServices));
            
        }

        public async Task<PostResponseDto> CreatePostAsync(CreatePostDto dto, string userId,string image)
        {
            var user = await GetUserIfExistsAsync(userId);
            if (user == null)
                throw new UserNotFoundException($"User with ID '{userId}' not found.");

            var post = _mapper.Map<Post>(dto);
            post.PostImage = image;
            post.UserId = userId;
            await _genericRepository.InserAsync(post);
            post.AddDomainEvent(new PostAddedDomainEvent(_mapper.Map<PostResponseDto>(post)));
            await _genericRepository.SaveChangesAsync();
            return _mapper.Map<PostResponseDto>(post);
        }

        public async Task DeletePost(Guid postId, string userId)
        {
            await EnsureUserExistsAsync(userId);

            var post = await _genericRepository.GetByIdAsync(postId);
            if (post == null)
                throw new PostNotFoundException($"Post with ID '{postId}' not found.");

            if (!IsPostOwner(post, userId))
                throw new PostForbiddenException("Unauthorized to delete this post.");

            _genericRepository.Delete(post);
            await _genericRepository.SaveChangesAsync();
        }

        public async Task<PostResponseDto> GetPostByIdAsync(Guid postId)
        {
            var post = await _genericRepository.Include(x => x.User).FirstOrDefaultAsync(p => p.Id == postId);
            if (post == null)
                throw new PostNotFoundException($"Post with ID '{postId}' not found.");

            return _mapper.Map<PostResponseDto>(post);
        }

        public async Task<PostResponseDto> UpdatePostsAsync(UpdatePostDto dto, string userId, Guid postId)
        {
            await EnsureUserExistsAsync(userId);

            var post = await _genericRepository.GetByIdAsync(postId);
            if (post == null)
                throw new PostNotFoundException($"Post with ID '{postId}' not found.");

            if (!IsPostOwner(post, userId))
                throw new PostForbiddenException("Forbidden, invalid credentials.");

            _mapper.Map(dto, post); // Update properties of the existing post object
            _genericRepository.Update(post);
            await _genericRepository.SaveChangesAsync();
            return _mapper.Map<PostResponseDto>(post);
        }

        private async Task<UserResponseDto> GetUserIfExistsAsync(string userId)
        {
            return await _userServices.GetUserByIdAsync(userId);
        }

        private async Task EnsureUserExistsAsync(string userId)
        {
            var user = await _userServices.GetUserByIdAsync(userId);
            if (user == null)
                throw new UserNotFoundException($"User with ID '{userId}' not found.");
        }

        private bool IsPostOwner(Post post, string userId)
        {
            return post.UserId == userId;
        }

        public  PaginationResponseDto<PostResponseDto> PostPaginationAsync(int pageSize, int page)
        {

            var posts =  _genericRepository.Include(p=>p.User).Include(p=>p.Comments);
            var paging =  _genericRepository.GetPagination(posts, page, pageSize).OrderByDescending(p=>p.CreateAt);
            return new PaginationResponseDto<PostResponseDto>(
                page,
                pageSize,
                posts.Count(),
                _mapper.Map<IEnumerable<PostResponseDto>>(paging)
                );
        }
    }
}
