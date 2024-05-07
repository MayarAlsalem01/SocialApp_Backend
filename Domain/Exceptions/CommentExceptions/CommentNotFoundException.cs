using SocialApp.Domain.Exceptions.Common;

namespace SocialApp.Domain.Exceptions.CommentExceptions
{
    public sealed class CommentNotFoundException:NotFoundException
    {
        public CommentNotFoundException(string masseage=""):base(masseage)
        {

        }
    }
}
