// <copyright file="DeleteExperienceCommand.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Experience.Commands
{
    using MediatR;

    /// <summary>
    /// The DeleteExperienceCommand class represents a command to delete an experience record by its unique identifier.
    /// </summary>
    public class DeleteExperienceCommand : IRequest<Unit>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteExperienceCommand"/> class.
        /// </summary>
        /// <param name="id">The Id.</param>
        public DeleteExperienceCommand(Guid id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets or sets the unique identifier of the experience record to delete.
        /// </summary>
        public Guid Id { get; set; }
    }
}
