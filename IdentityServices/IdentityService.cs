using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SocialApp.data;
using SocialApp.Domain.Entities;
using SocialApp.Domain.Exceptions.RoleExceptions;
using SocialApp.Domain.Exceptions.UserExceptions;
using SocialApp.Dtos.Response;
using SocialApp.Dtos.UserDto;
using SocialApp.IServices;
using SocialApp.IServices.UserServices;

namespace SocialApp.IdentityServices
{
    public class IdentityService : IIdendtityServices
    {
        private readonly UserManager<User> _userManger;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly ITokenServices _tokenServices;
        private readonly IUserServices _userServices;
        private readonly ApplicationDbContext _context;

        public IdentityService(UserManager<User> userManger, SignInManager<User> signInManager, IMapper mapper, ITokenServices tokenServices, IUserServices userServices, ApplicationDbContext context)
        {
            _userManger = userManger;
            _signInManager = signInManager;
            _mapper = mapper;
            _tokenServices = tokenServices;
            _userServices = userServices;
            _context = context;
        }

        public async Task<AuthDto> CreateAsync(CreateUserDto userDto)
        {
            using(var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var user = await _userManger.FindByEmailAsync(userDto.Email);
                    if (user is not null) throw new UserAlreadyExistException();
                    user = _mapper.Map<User>(userDto);
                    var result = await _userManger.CreateAsync(user, userDto.Password);
                    if (!result.Succeeded)
                    {
                        throw new UserBadRequestException("bad", result.Errors.ToDictionary(res => res.Code, message => message.Description));


                    }


                    var userCreated = _mapper.Map<AuthDto>(user);
                    var userAsignToRole = await _userServices.AddUserToRoleAsync("user", user.Id);
                    var roles = await GetRolesAsync(user);
                    userCreated.Token = _tokenServices.GenerateJwtToken(user, userAsignToRole.Roles.ToList());
                    transaction.Commit();
                    return userCreated;
                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                    throw ; 
                }
            }
            //    var user = await _userManger.FindByEmailAsync(userDto.Email);
            //if (user is not null) throw new UserAlreadyExistException();
            //user = _mapper.Map<User>(userDto);
            //var result = await _userManger.CreateAsync(user, userDto.Password);
            //if (!result.Succeeded)
            //{
            //    throw new UserBadRequestException("bad", result.Errors.ToDictionary(res => res.Code, message => message.Description));
              
               
            //}
            
            
            //var userCreated =_mapper.Map<AuthDto>(user);
            //var userAsignToRole=await _userServices.AddUserToRoleAsync("user", user.Id);
            //var roles = await GetRolesAsync(user);
            //userCreated.Token=  _tokenServices.GenerateJwtToken(user, userAsignToRole.Roles.ToList());
            //return userCreated;
        }

        public async Task<List<string>> GetRolesAsync(User user)
        {
            var roles = await _userManger.GetRolesAsync(user);
            return roles.ToList();
        }

        public async Task<AuthDto> LognAsync(LoginUserDto loginUserDto)
        {
            var user = await _userManger.FindByEmailAsync(loginUserDto.Email);
            if (user is null) throw new InvalidCredentialsException();
           var signInUser= await _signInManager.CheckPasswordSignInAsync(user, loginUserDto.Password, false);
            if (!signInUser.Succeeded) throw new InvalidCredentialsException();
            var roles = await GetRolesAsync(user);
            var token = _tokenServices.GenerateJwtToken(user,roles);
            return new AuthDto() { Email = user.Email, UserName = user.UserName,Token=token,ImageUrl=user.ImageUrl ,FirstName=user.FirstName,LastName=user.LastName};


        }
       

    }
}
