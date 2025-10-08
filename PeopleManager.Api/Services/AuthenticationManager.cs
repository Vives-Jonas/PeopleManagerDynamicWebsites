using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Identity;
using PeopleManager.Api.Settings;
using Microsoft.IdentityModel.Tokens;

namespace PeopleManager.Api.Services
{
    public class AuthenticationManager(JwtSettings jwtSettings)
    {
        public string GenerateJwtToken(IdentityUser user)
        {
            var handler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);

            
                var tokenDescriptor = new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(new[]
                    {
                        new System.Security.Claims.Claim(JwtRegisteredClaimNames.Sub, user.Id),
                        new System.Security.Claims.Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                        new System.Security.Claims.Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    }),
                    Expires = DateTime.UtcNow.Add(jwtSettings.ExpirationPeriod),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                };
                var token = handler.CreateToken(tokenDescriptor);
                return handler.WriteToken(token);
            
        }
    }
}
