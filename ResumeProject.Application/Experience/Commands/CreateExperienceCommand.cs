// <copyright file="CreateExperienceCommand.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Experience.Commands
{
    using MediatR;
    using ResumeProject.Domain.Entities;

    /// <summary>
    /// Command to create a new Experience entry.
    /// </summary>
    public class CreateExperienceCommand : IRequest<Experience>
    {
        /// <summary>
        /// Gets or sets the job position of the experience.
        /// </summary>
        public string Position { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the company name.
        /// </summary>
        public string Company { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the start date of the experience.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date. Nullable if the job is current.
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets the description of duties or achievements.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the associated resume ID.
        /// </summary>
        public Guid ResumeId { get; set; }
    }
}
