namespace SocialApp.Domain.Exceptions.Common
{
    public class NotFoundException:BaseException
    {
        public NotFoundException(string message=""):base(message)
        {
            StatusCode = 404;
        }
    }
}
