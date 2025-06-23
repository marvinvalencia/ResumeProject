// <copyright file="Education.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ResumeProject.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;
    using ResumeProject.Domain.Interfaces;

    /// <summary>
    /// Education Entity representing an individual's educational background.
    /// </summary>
    public class Education : IEntityBase
    {
        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the resume identifier this education belongs to.
        /// </summary>
        public Guid ResumeId { get; set; }

        /// <summary>
        /// Gets or sets the degree obtained.
        /// </summary>
        public string Degree { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the institution name.
        /// </summary>
        public string Institution { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the start date of the education.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the education.
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets the major or field of study.
        /// </summary>
        public string Major { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the GPA (Grade Point Average).
        /// </summary>
        public double GPA { get; set; }

        /// <summary>
        /// Gets or sets the resume this education belongs to.
        /// </summary>
        public Resume? Resume { get; set; } = null;
    }
}
