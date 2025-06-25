// <copyright file="UpdateExperienceCommand.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Experience.Commands
{
    using MediatR;

    /// <summary>
    /// Command to update an existing experience.
    /// </summary>
    public class UpdateExperienceCommand : IRequest<Unit>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the experience to update.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the position held during the experience.
        /// </summary>
        public string? Position { get; set; } = null;

        /// <summary>
        /// Gets or sets the name of the company where the experience was gained.
        /// </summary>
        public string? Company { get; set; } = null;

        /// <summary>
        /// Gets or sets the start date of the experience.
        /// </summary>
        public DateTime? StartDate { get; set; } = null;

        /// <summary>
        /// Gets or sets the end date of the experience. This can be null if the experience is ongoing.
        /// </summary>
        public DateTime? EndDate { get; set; } = null;

        /// <summary>
        /// Gets or sets the description of the experience.
        /// </summary>
        public string? Description { get; set; } = null;

        /// <summary>
        /// Gets or sets the unique identifier of the resume associated with this experience.
        /// </summary>
        public Guid? ResumeId { get; set; } = null;
    }
}
