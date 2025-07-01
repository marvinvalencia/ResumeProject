// <copyright file="Resume.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;
    using ResumeProject.Domain.Interfaces;

    /// <summary>
    /// The Resume class represents a job seeker's resume, containing personal information, skills, experiences, and education.
    /// </summary>
    public class Resume : IEntityBase
    {
        /// <summary>
        /// Gets or sets the unique identifier for the resume.
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the picture associated with the resume.
        /// </summary>
        public byte[] Picture { get; set; } = null!;

        /// <summary>
        /// Gets or sets the first name of the individual.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the last name of the individual.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email address of the individual.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the phone number of the individual.
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the address of the individual.
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the summary or objective statement of the resume.
        /// </summary>
        public string Summary { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the collection of skills associated with the resume.
        /// </summary>
        public ICollection<Skill> Skills { get; set; } = new List<Skill>();

        /// <summary>
        /// Gets or sets the collection of experiences associated with the resume.
        /// </summary>
        public ICollection<Experience> Experiences { get; set; } = new List<Experience>();

        /// <summary>
        /// Gets or sets the collection of educations associated with the resume.
        /// </summary>
        public ICollection<Education> Educations { get; set; } = new List<Education>();

        /// <summary>
        /// Gets or sets the collection of links associated with the resume, such as social media profiles or personal websites.
        /// </summary>
        public ICollection<Link> Links { get; set; } = new List<Link>();

        /// <summary>
        /// Gets or sets the interests or hobbies of the individual.
        /// </summary>
        public string Interests { get; set; } = string.Empty;
    }
}
