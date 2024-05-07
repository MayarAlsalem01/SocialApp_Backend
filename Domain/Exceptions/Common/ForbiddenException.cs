namespace SocialApp.Domain.Exceptions.Common
{
    public  class ForbiddenException:BaseException
    {
        public ForbiddenException(string message=""):base(message)
        {

        }
    }
}
