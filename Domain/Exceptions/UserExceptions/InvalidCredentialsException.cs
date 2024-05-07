using SocialApp.Domain.Exceptions.Common;

namespace SocialApp.Domain.Exceptions.UserExceptions
{
    public class InvalidCredentialsException:UnauthorizedException
    {
        public InvalidCredentialsException(string message= "Invalid email or password.") : base(message)
        
        {

        }
    }
}
