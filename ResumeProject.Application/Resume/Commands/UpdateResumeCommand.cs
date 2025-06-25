// <copyright file="UpdateResumeCommand.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

#pragma warning disable SA1011 // Closing square brackets should be spaced correctly

namespace ResumeProject.Application.Resume.Commands
{
    using MediatR;

    /// <summary>
    /// The UpdateResumeCommand class represents a command to update an existing resume in the database.
    /// </summary>
    public class UpdateResumeCommand : IRequest<Unit>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the resume to update.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the picture associated with the resume.
        /// </summary>
        public byte[]? Picture { get; set; } = null!;

        /// <summary>
        /// Gets or sets the first name of the individual.
        /// </summary>
        public string? FirstName { get; set; } = null;

        /// <summary>
        /// Gets or sets the last name of the individual.
        /// </summary>
        public string? LastName { get; set; } = null;

        /// <summary>
        /// Gets or sets the email address of the individual.
        /// </summary>
        public string? Email { get; set; } = null;

        /// <summary>
        /// Gets or sets the phone number of the individual.
        /// </summary>
        public string? PhoneNumber { get; set; } = null;

        /// <summary>
        /// Gets or sets the address of the individual.
        /// </summary>
        public string? Address { get; set; } = null;

        /// <summary>
        /// Gets or sets the summary or objective statement of the resume.
        /// </summary>
        public string? Summary { get; set; } = null;

        /// <summary>
        /// Gets or sets the interests or hobbies of the individual.
        /// </summary>
        public string? Interests { get; set; } = null;
    }
}

#pragma warning restore SA1011 // Closing square brackets should be spaced correctly
