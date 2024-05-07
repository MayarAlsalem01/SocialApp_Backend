using SocialApp.Domain.Exceptions.Common;

namespace SocialApp.Domain.Exceptions.PostExceptions
{
    sealed public class PostForbiddenException: ForbiddenException
    {
        public PostForbiddenException(string message):base(message)
        {
                
        }
    }
}
