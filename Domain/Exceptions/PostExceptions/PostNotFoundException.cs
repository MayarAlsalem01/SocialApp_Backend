using SocialApp.Domain.Exceptions.Common;

namespace SocialApp.Domain.Exceptions.PostExceptions
{
    sealed public class PostNotFoundException:NotFoundException
    {
        public PostNotFoundException(string message="post not found "):base(message)
        {

        }
    }
}
