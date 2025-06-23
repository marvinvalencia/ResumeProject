using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ResumeProject.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ResumeProject.Application.Services
{
    public class TokenService
    {
        private readonly string jwtSecret;
        private readonly string jwtIssuer;
        private readonly string jwtAudience;

        public TokenService(IConfiguration configuration)
        {
            this.jwtSecret = configuration["JWT_SECRET"] ?? throw new InvalidOperationException("JWT_SECRET not found.");
            this.jwtIssuer = configuration["JWT_ISSUER"] ?? throw new InvalidOperationException("JWT_ISSUER not found.");
            this.jwtAudience = configuration["JWT_AUDIENCE"] ?? throw new InvalidOperationException("JWT_AUDIENCE not found.");
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            if (string.IsNullOrEmpty(this.jwtSecret) || this.jwtSecret.Length < 32)
            {
                throw new Exception("JWT secret must be at least 32 characters long");
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.jwtSecret));

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(60),
                Issuer = this.jwtIssuer,
                Audience = this.jwtAudience,
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
