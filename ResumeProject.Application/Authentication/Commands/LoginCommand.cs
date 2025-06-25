// <copyright file="LoginCommand.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Authentication.Commands
{
    using MediatR;
    using ResumeProject.Application.Authentication.DTOs;

    /// <summary>
    /// The LoginCommand class represents a command for user login, containing the user's email and password.
    /// </summary>
    public class LoginCommand : IRequest<LoginResponseDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginCommand"/> class with the specified email and password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        public LoginCommand(string email, string password)
        {
            this.Email = email;
            this.Password = password;
        }

        /// <summary>
        /// Gets the email address of the user attempting to log in.
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// Gets the password of the user attempting to log in.
        /// </summary>
        public string Password { get; }
    }
}