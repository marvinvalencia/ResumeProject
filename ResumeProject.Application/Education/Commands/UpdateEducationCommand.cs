// <copyright file="UpdateEducationCommand.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Education.Commands
{
    using MediatR;

    /// <summary>
    /// The UpdateEducationCommand class represents a command to update an existing education record in the database.
    /// </summary>
    public class UpdateEducationCommand : IRequest<Unit>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the education record to update.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the degree obtained during the education.
        /// </summary>
        public string? Degree { get; set; } = null;

        /// <summary>
        /// Gets or sets the name of the institution where the education was completed.
        /// </summary>
        public string? Institution { get; set; } = null;

        /// <summary>
        /// Gets or sets the start date of the education period.
        /// </summary>
        public DateTime? StartDate { get; set; } = null;

        /// <summary>
        /// Gets or sets the end date of the education period. This can be null if the education is ongoing.
        /// </summary>
        public DateTime? EndDate { get; set; } = null;

        /// <summary>
        /// Gets or sets the major or field of study for the education.
        /// </summary>
        public string? Major { get; set; } = null;

        /// <summary>
        /// Gets or sets the description of the education, providing additional context or details.
        /// </summary>
        public string? Description { get; set; } = null;

        /// <summary>
        /// Gets or sets the GPA (Grade Point Average) achieved during the education.
        /// </summary>
        public double? GPA { get; set; } = null;

        /// <summary>
        /// Gets or sets the unique identifier of the resume associated with this skill.
        /// </summary>
        public Guid? ResumeId { get; set; } = null;
    }
}
