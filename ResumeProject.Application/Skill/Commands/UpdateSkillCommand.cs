// <copyright file="UpdateSkillCommand.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Skill.Commands
{
    using MediatR;

    /// <summary>
    /// The UpdateSkillCommand class represents a command to update an existing skill in the database.
    /// </summary>
    public class UpdateSkillCommand : IRequest<Unit>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the skill to update.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the skill to update.
        /// </summary>
        public string? Name { get; set; } = null;

        /// <summary>
        /// Gets or sets the proficiency level of the skill.
        /// </summary>
        public int? Proficiency { get; set; } = null;

        /// <summary>
        /// Gets or sets the years of experience with the skill.
        /// </summary>
        public int? YearsOfExperience { get; set; } = null;

        /// <summary>
        /// Gets or sets the unique identifier of the resume associated with this skill.
        /// </summary>
        public Guid? ResumeId { get; set; } = null;
    }
}
