using SocialApp.Domain.Exceptions.Common;

namespace SocialApp.Domain.Exceptions.UserExceptions
{
    public class UserAlreadyExistException:AlreadyExistException
    {
        public UserAlreadyExistException(string message="User Already Exsist"):base(message)
        {

        }
    }
}
