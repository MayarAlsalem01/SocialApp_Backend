namespace SocialApp.Domain.Exceptions
{
    public class BaseException:Exception

    {
        public int StatusCode { get;protected set; }
        public BaseException(string message=""):base(message)
        {

        }
    }
}
