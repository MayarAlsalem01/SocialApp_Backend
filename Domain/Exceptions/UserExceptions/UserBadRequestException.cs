using SocialApp.Domain.Exceptions.Common;
using SocialApp.Dtos.Response;

namespace SocialApp.Domain.Exceptions.UserExceptions
{
    public class UserBadRequestException: BadRequestException
    {
       
        public UserBadRequestException(string message):base(message)
        {
           

        }
        public UserBadRequestException(string message,Dictionary<string,string>errors) : base(message)
        {
            this.errors = errors;

        }
    }
}
