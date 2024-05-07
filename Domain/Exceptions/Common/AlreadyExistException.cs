namespace SocialApp.Domain.Exceptions.Common
{
    public class AlreadyExistException:BaseException
    {
        public AlreadyExistException(string message):base(message)
        {
            StatusCode = 403;
        }
    }
}
