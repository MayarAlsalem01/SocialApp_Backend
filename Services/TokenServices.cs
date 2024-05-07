using Microsoft.IdentityModel.Tokens;
using SocialApp.Domain.Entities;
using SocialApp.IServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SocialApp.Services
{
    sealed public class JWT
    {
        public static string Issuer { get; set; } = string.Empty;
        public static string Audience { get; set; } = string.Empty;
        public static string Key { get; set; } = string.Empty;
    }
    public class TokenServices:ITokenServices
    {

       
        public TokenServices()
        {
           
        }
        public string GenerateJwtToken(User user, List<string> userRoles)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWT.Key));
            var credntails = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Id),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(ClaimTypes.Role,"user"),
                new Claim(ClaimTypes.Name,user.Id)
            };
      
             
            //claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));
               

            var token = new JwtSecurityToken
                (
                    issuer: JWT.Issuer,
                    audience: JWT.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(1),
                   signingCredentials: credntails
                );
            return  new JwtSecurityTokenHandler().WriteToken(token);
        }
       
    }
}
