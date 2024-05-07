namespace SocialApp.Domain.Exceptions.Common
{
    public class UnauthorizedException:BaseException
    {
        public UnauthorizedException(string message=""):base(message)
        {
            StatusCode = 401;
        }
    }
}
