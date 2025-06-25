// <copyright file="GetEducationByIdQuery.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Education.Queries
{
    using MediatR;
    using ResumeProject.Domain.Entities;

    /// <summary>
    /// The GetEducationByIdQuery class represents a query to retrieve a specific education record by its unique identifier.
    /// </summary>
    public class GetEducationByIdQuery : IRequest<Education?>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetEducationByIdQuery"/> class.
        /// </summary>
        /// <param name="id">The Id.</param>
        public GetEducationByIdQuery(Guid id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets or sets the unique identifier of the education record to retrieve.
        /// </summary>
        public Guid Id { get; set; }
    }
}
