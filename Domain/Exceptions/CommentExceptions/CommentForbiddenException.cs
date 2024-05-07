using SocialApp.Domain.Exceptions.Common;

namespace SocialApp.Domain.Exceptions.CommentExceptions
{
    public sealed class CommentForbiddenException: ForbiddenException
    {
        public CommentForbiddenException(string message):base(message)
        {

        }
    }
}
