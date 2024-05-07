using SocialApp.Domain.Entities;

namespace SocialApp.IServices
{
    public interface ITokenServices
    {
        string GenerateJwtToken(User user, List<string> userRoles );
    }
}
