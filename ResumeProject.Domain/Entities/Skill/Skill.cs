// <copyright file="Skill.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;
    using ResumeProject.Domain.Interfaces;

    /// <summary>
    /// The Skill class represents a specific skill or expertise of an individual in a resume.
    /// </summary>
    public class Skill : IEntityBase
    {
        /// <summary>
        /// Gets or sets the unique identifier for the skill.
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the identifier of the resume this skill belongs to.
        /// </summary>
        public Guid ResumeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the skill.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the proficiency of the skill.
        /// </summary>
        public int Proficiency { get; set; }

        /// <summary>
        /// Gets or sets the number of years of experience with this skill.
        /// </summary>
        public int YearsOfExperience { get; set; }

        /// <summary>
        /// Gets or sets the resume this skill belongs to.
        /// </summary>
        public Resume? Resume { get; set; } = null;
    }
}
