// <copyright file="Link.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Domain.Entities
{
    /// <summary>
    /// The Link class represents a hyperlink associated with a resume, such as social media profiles or personal websites.
    /// </summary>
    public class Link
    {
        /// <summary>
        /// Gets or sets the unique identifier for the link.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the identifier of the resume this experience belongs to.
        /// </summary>
        public Guid ResumeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the link, such as "GitHub", "LinkedIn", etc.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the URL of the link.
        /// </summary>
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets Resume this experience belongs to.
        /// </summary>
        public Resume? Resume { get; set; } = null;
    }
}
