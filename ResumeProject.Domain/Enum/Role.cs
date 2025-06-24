// <copyright file="Role.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Domain.Enum
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// The Role enum defines the different roles a user can have in the system.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Role
    {
        /// <summary>
        /// The Admin role has full access to the system, including user management and configuration.
        /// </summary>
        Admin,

        /// <summary>
        /// The User role has limited access, primarily to manage their own resume and personal information.
        /// </summary>
        User,
    }
}
