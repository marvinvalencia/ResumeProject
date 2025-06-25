// <copyright file="LoginRequestDto.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Authentication.DTOs
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The LoginDto class represents the data transfer object for user login, containing email and password fields.
    /// </summary>
    public class LoginRequestDto
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
    }
}
