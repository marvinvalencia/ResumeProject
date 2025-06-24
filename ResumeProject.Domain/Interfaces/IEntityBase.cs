// <copyright file="IEntityBase.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Domain.Interfaces
{
    /// <summary>
    /// The IEntityBase interface defines a base entity with a unique identifier.
    /// </summary>
    public interface IEntityBase
    {
        /// <summary>
        /// Gets or sets the unique identifier for the entity.
        /// </summary>
        Guid Id { get; set; }
    }
}
