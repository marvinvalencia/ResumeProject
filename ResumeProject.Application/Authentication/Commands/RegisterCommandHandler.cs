// <copyright file="RegisterCommandHandler.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Authentication.Commands
{
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using ResumeProject.Domain.Entities;
    using ResumeProject.Domain.Enum;

    /// <summary>
    /// The RegisterCommandHandler class handles the registration of a new user, validating the input and creating the user in the system.
    /// </summary>
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterCommandHandler"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="roleManager">The role manager.</param>
        public RegisterCommandHandler(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        /// <summary>
        /// The Handle method processes the registration command, validating the input and creating a new user in the system.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result.</returns>
        /// <exception cref="ArgumentException">The argument exception.</exception>
        /// <exception cref="InvalidOperationException">The invalid operation exception.</exception>
        /// <exception cref="Exception">The exception.</exception>
        public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (request.Password != request.ConfirmPassword)
            {
                throw new ArgumentException("Passwords do not match.");
            }

            var existingUser = await this.userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("User already exists.");
            }

            var user = new User
            {
                Email = request.Email,
                UserName = request.Email,
            };

            var result = await this.userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errorMessage = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new Exception($"User creation failed: {errorMessage}");
            }

            var role = Role.User.ToString();
            if (!await this.roleManager.RoleExistsAsync(role))
            {
                await this.roleManager.CreateAsync(new IdentityRole(role));
            }

            await this.userManager.AddToRoleAsync(user, role);

            return "User registered successfully.";
        }
    }
}
