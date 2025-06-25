// <copyright file="GetResumeByIdQuery.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Resume.Queries
{
    using MediatR;
    using ResumeProject.Domain.Entities;

    /// <summary>
    /// The GetResumeByIdQuery class represents a query to retrieve a specific resume record by its unique identifier.
    /// </summary>
    public class GetResumeByIdQuery : IRequest<Resume?>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetResumeByIdQuery"/> class.
        /// </summary>
        /// <param name="id">The Id.</param>
        public GetResumeByIdQuery(Guid id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets or sets the unique identifier of the resume record to retrieve.
        /// </summary>
        public Guid Id { get; set; }
    }
}
