// <copyright file="DeleteSkillCommand.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Skill.Commands
{
    using MediatR;

    /// <summary>
    /// The DeleteSkillCommand class represents a command to delete an existing skill record from the database.
    /// </summary>
    public class DeleteSkillCommand : IRequest<Unit>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteSkillCommand"/> class.
        /// </summary>
        /// <param name="id">The Id.</param>
        public DeleteSkillCommand(Guid id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets or sets the unique identifier of the skill record to delete.
        /// </summary>
        public Guid Id { get; set; }
    }
}
