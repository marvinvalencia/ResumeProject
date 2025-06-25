// <copyright file="CreateResumeCommand.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Resume.Commands
{
    using MediatR;
    using ResumeProject.Domain.Entities;

    /// <summary>
    /// The CreateResumeCommand class represents a command to create a new resume in the database.
    /// </summary>
    public class CreateResumeCommand : IRequest<Resume>
    {
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
        /// Gets or sets the interests or hobbies of the individual.
        /// </summary>
        public string Interests { get; set; } = string.Empty;
    }
}
