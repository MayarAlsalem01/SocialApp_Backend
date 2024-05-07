using SocialApp.Domain.Exceptions.Common;

namespace SocialApp.Domain.Exceptions.UserExceptions
{
    public class UserNotFoundException:NotFoundException
    {
        public UserNotFoundException(string message=""):base(message)
        {

        }
    }
}
