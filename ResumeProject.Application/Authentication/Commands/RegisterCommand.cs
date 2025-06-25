// <copyright file="RegisterCommand.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Authentication.Commands
{
    using MediatR;

    /// <summary>
    /// The RegisterCommand class represents a command for user registration, containing the user's email, password, and confirmation password.
    /// </summary>
    public class RegisterCommand : IRequest<string>
    {
        /// <summary>
        /// Gets the email address of the user registering.
        /// </summary>
        public string Email { get; init; } = string.Empty;

        /// <summary>
        /// Gets the password for the user registering.
        /// </summary>
        public string Password { get; init; } = string.Empty;

        /// <summary>
        /// Gets the confirmation password for the user, used to ensure the user has entered the password correctly.
        /// </summary>
        public string ConfirmPassword { get; init; } = string.Empty;
    }
}
