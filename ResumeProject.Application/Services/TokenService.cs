using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ResumeProject.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text;

namespace ResumeProject.Application.Services
{
    public class TokenService
    {
        private readonly string jwtSecret;

        public TokenService(IConfiguration configuration)
        {
            this.jwtSecret = configuration["JWT_SECRET"] ?? throw new InvalidOperationException("JWT_SECRET not found.");
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            if (string.IsNullOrEmpty(this.jwtSecret) || this.jwtSecret.Length < 32)
            {
                throw new Exception("JWT secret must be at least 32 characters long");
            }

            var key = Encoding.UTF8.GetBytes(this.jwtSecret);

            var claims = new List<Claim>()
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Email, user.Email),
                new(ClaimTypes.Role, user.Role.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(60),
                Issuer = "ResumeProjectIssuer",
                Audience = "ResumeProjectAudience",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
