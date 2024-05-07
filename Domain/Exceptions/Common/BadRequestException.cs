namespace SocialApp.Domain.Exceptions.Common
{
    public class BadRequestException:BaseException
    {
        public Dictionary<string, string> errors { get; set; } = new Dictionary<string, string>();
        public BadRequestException(string message):base(message)
        {

        }
    }
}
