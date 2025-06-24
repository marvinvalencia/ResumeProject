// <copyright file="LoginResponseDto.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.DTOs
{
    /// <summary>
    /// The LoginDto class represents the data transfer object for user login, containing email and password fields.
    /// </summary>
    public class LoginResponseDto
    {
        /// <summary>
        /// Gets or sets the status of the login operation.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the token for the user.
        /// </summary>
        public string Token { get; set; } = string.Empty;
    }
}
