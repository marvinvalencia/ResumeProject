// <copyright file="DeleteResumeCommand.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Resume.Commands
{
    using MediatR;

    /// <summary>
    /// The DeleteResumeCommand class represents a command to delete an existing resume from the database.
    /// </summary>
    public class DeleteResumeCommand : IRequest<Unit>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteResumeCommand"/> class.
        /// </summary>
        /// <param name="id">The Id.</param>
        public DeleteResumeCommand(Guid id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets or sets the unique identifier of the resume to delete.
        /// </summary>
        public Guid Id { get; set; }
    }
}
