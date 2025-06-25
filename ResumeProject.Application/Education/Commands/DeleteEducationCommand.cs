// <copyright file="DeleteEducationCommand.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Education.Commands
{
    using MediatR;

    /// <summary>
    /// The DeleteEducationCommand class represents a command to delete an existing education record from the database.
    /// </summary>
    public class DeleteEducationCommand : IRequest<Unit>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteEducationCommand"/> class.
        /// </summary>
        /// <param name="id">The Id.</param>
        public DeleteEducationCommand(Guid id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets or sets the unique identifier of the education record to delete.
        /// </summary>
        public Guid Id { get; set; }
    }
}
