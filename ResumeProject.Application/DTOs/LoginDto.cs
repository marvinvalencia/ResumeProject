// <copyright file="LoginDto.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.DTOs
{
    /// <summary>
    /// The LoginDto class represents the data transfer object for user login, containing email and password fields.
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password for the user.
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
