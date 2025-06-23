// <copyright file="Experience.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ResumeProject.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;
    using ResumeProject.Domain.Interfaces;

    /// <summary>
    /// This class represents an individual's work experience in a resume.
    /// </summary>
    public class Experience : IEntityBase
    {
        /// <summary>
        /// Gets or sets the unique identifier for the experience entry.
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the identifier of the resume this experience belongs to.
        /// </summary>
        public Guid ResumeId { get; set; }

        /// <summary>
        /// Gets or sets the position or job title held during this experience.
        /// </summary>
        public string Position { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the company where the experience was gained.
        /// </summary>
        public string Company { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets job responsibilities or key achievements during this experience.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the start date of the experience.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the experience. This can be null if the position is current.
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets Resume this experience belongs to.
        /// </summary>
        public Resume? Resume { get; set; } = null;
    }
}
