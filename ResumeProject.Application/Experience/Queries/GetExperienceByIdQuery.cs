// <copyright file="GetExperienceByIdQuery.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Experience.Queries
{
    using MediatR;
    using ResumeProject.Domain.Entities;

    /// <summary>
    /// The GetEducationByIdQuery class represents a query to retrieve a specific experience record by its unique identifier.
    /// </summary>
    public class GetExperienceByIdQuery : IRequest<Experience?>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetExperienceByIdQuery"/> class.
        /// </summary>
        /// <param name="id">The Id.</param>
        public GetExperienceByIdQuery(Guid id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets or sets the unique identifier of the Experience record to retrieve.
        /// </summary>
        public Guid Id { get; set; }
    }
}
