// <copyright file="LoginCommandHandler.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Commands
{
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using ResumeProject.Application.DTOs;
    using ResumeProject.Application.Services;
    using ResumeProject.Domain.Entities;

    /// <summary>
    /// The LoginCommandHandler class handles the login command, validating user credentials and generating a JWT token.
    /// </summary>
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDto>
    {
        private readonly UserManager<User> userManager;
        private readonly TokenService tokenService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginCommandHandler"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="tokenService">The token service.</param>
        public LoginCommandHandler(UserManager<User> userManager, TokenService tokenService)
        {
            this.userManager = userManager;
            this.tokenService = tokenService;
        }

        /// <summary>
        /// The Handle method processes the login command, authenticating the user and returning a response with a JWT token.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The login response dto.</returns>
        /// <exception cref="UnauthorizedAccessException">The exception.</exception>
        public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await this.userManager.FindByEmailAsync(request.Email);
            if (user == null || !await this.userManager.CheckPasswordAsync(user, request.Password))
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            var token = this.tokenService.GenerateToken(user);
            return new LoginResponseDto
            {
                Message = "Successfully signed in.",
                Token = token,
            };
        }
    }
}
