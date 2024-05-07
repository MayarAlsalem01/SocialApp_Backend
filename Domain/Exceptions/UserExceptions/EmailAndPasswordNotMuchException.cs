using SocialApp.Domain.Exceptions.Common;

namespace SocialApp.Domain.Exceptions.UserExceptions
{
    public class EmailAndPasswordNotMuchException: UnauthorizedException
    {
        public EmailAndPasswordNotMuchException(string message=""):base(message)
        {

        }
    }
}
