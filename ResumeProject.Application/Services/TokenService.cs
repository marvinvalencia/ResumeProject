// <copyright file="TokenService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Services
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using ResumeProject.Domain.Entities;

    /// <summary>
    /// The TokenService class is responsible for generating JWT tokens for users.
    /// </summary>
    public class TokenService
    {
        private readonly string jwtSecret;
        private readonly string jwtIssuer;
        private readonly string jwtAudience;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenService"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <exception cref="InvalidOperationException">The exception.</exception>
        public TokenService(IConfiguration configuration)
        {
            this.jwtSecret = configuration["JWT_SECRET"] ?? throw new InvalidOperationException("JWT_SECRET not found.");
            this.jwtIssuer = configuration["JWT_ISSUER"] ?? throw new InvalidOperationException("JWT_ISSUER not found.");
            this.jwtAudience = configuration["JWT_AUDIENCE"] ?? throw new InvalidOperationException("JWT_AUDIENCE not found.");
        }

        /// <summary>
        /// The GenerateToken method generates a JWT token for the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The token.</returns>
        /// <exception cref="Exception">The exception.</exception>
        /// <exception cref="InvalidOperationException">The invalid operation exception.</exception>
        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            if (string.IsNullOrEmpty(this.jwtSecret) || this.jwtSecret.Length < 32)
            {
                throw new Exception("JWT secret must be at least 32 characters long");
            }

            if (string.IsNullOrWhiteSpace(user.Email))
            {
                throw new InvalidOperationException("No email address was provided for the user.");
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.jwtSecret));

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(60),
                Issuer = this.jwtIssuer,
                Audience = this.jwtAudience,
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
