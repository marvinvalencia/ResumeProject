// <copyright file="RegisterDto.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Authentication.DTOs
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The RegisterDto class represents the data transfer object for user registration, containing email, password, and confirm password fields.
    /// </summary>
    public class RegisterDto
    {
        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password for the user.
        /// </summary>
        [Required]
        [PasswordPropertyText]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the confirmation password for the user, used to ensure the user has entered the password correctly.
        /// </summary>
        [Required]
        [PasswordPropertyText]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
