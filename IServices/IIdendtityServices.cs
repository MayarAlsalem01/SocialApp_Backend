using SocialApp.Domain.Entities;
using SocialApp.Dtos.UserDto;
using System.Threading.Tasks;

namespace SocialApp.IServices
{
    /// <summary>
    /// Defines operations related to user management.
    /// </summary>
    public interface IIdendtityServices
    {
        /// <summary>
        /// Creates a new user asynchronously.
        /// </summary>
        /// <param name="userDto">The user DTO containing user information.</param>
        /// <exception cref="UserAlreadyExistException">Thrown if a user with the provided email already exists.</exception>
        /// <returns>A task representing the asynchronous operation. The task result contains the response DTO for the created user.</returns>
        Task<AuthDto> CreateAsync(CreateUserDto userDto);

        /// <summary>
        /// Logs in a user asynchronously.
        /// </summary>
        /// <param name="loginUserDto">The login user DTO containing login credentials.</param>
        /// <exception cref="UserNotFoundException">Thrown if a user with the provided email does not exist.</exception>
        /// <exception cref="EmailAndPasswordNotMuchException">Thrown if the provided email and password do not match.</exception>
        /// <returns>A task representing the asynchronous operation. The task result contains the response DTO for the logged-in user.</returns>
        Task<AuthDto> LognAsync(LoginUserDto loginUserDto);

        Task<List<string>> GetRolesAsync(User user);
    }
}
