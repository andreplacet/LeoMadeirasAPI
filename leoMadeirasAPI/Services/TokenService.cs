using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using leoMadeirasAPI.Models;
using Microsoft.IdentityModel.Tokens;

namespace leoMadeirasAPI.Services
{
    public class TokenService
    {
        public static string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);

            DateTime expireTime = DateTime.UtcNow.AddMinutes(5);
            user.ExpireTokenDate = expireTime;

            var tokerDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username.ToString()),
                }),
                Expires = expireTime,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                                                                    SecurityAlgorithms.HmacSha256Signature)

            };

            var token = tokenHandler.CreateToken(tokerDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}