// <copyright file="User.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ResumeProject.Domain.Entities
{
    using Microsoft.AspNetCore.Identity;
    using ResumeProject.Domain.Enum;

    /// <summary>
    /// The User class represents a user in the system, inheriting from IdentityUser.
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        /// Gets or sets the role of the user in the system.
        /// </summary>
        public Role Role { get; set; }
    }
}
