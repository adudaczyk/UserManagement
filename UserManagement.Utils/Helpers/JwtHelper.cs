using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace UserManagement.API.Jwt
{
    public static class JwtHelper
    {
        private const int JWT_TOKEN_LIFETIME_IN_HOURS = 8;
        private const string CLAIMS_USERID = "userId";

        public static string GenerateToken(string secret, int userId)
        {
            var credentials = new SigningCredentials(GetSecurityKey(secret), SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddHours(JWT_TOKEN_LIFETIME_IN_HOURS);
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Expires = expiration,
                Subject = new ClaimsIdentity(new[] 
                {
                    new Claim(CLAIMS_USERID, userId.ToString())
                }),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static SymmetricSecurityKey GetSecurityKey(string secret)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
        }
    }
}
