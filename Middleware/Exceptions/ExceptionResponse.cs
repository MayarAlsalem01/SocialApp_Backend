using System.Net;

namespace SocialApp.Middleware.Exceptions
{
    public record ExceptionResponse(HttpStatusCode StatusCode, string Description,Dictionary<string,string>errors=null);
}
