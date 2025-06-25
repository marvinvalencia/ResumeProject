// <copyright file="GetSkillByIdQuery.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Skill.Queries
{
    using MediatR;
    using ResumeProject.Domain.Entities;

    /// <summary>
    /// The GetResumeByIdQuery class represents a query to retrieve a specific skill record by its unique identifier.
    /// </summary>
    public class GetSkillByIdQuery : IRequest<Skill?>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetSkillByIdQuery"/> class.
        /// </summary>
        /// <param name="id">The Id.</param>
        public GetSkillByIdQuery(Guid id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets or sets the unique identifier of the skill record to retrieve.
        /// </summary>
        public Guid Id { get; set; }
    }
}
