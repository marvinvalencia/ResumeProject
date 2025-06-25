// <copyright file="CreateSkillCommand.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Skill.Commands
{
    using MediatR;
    using ResumeProject.Domain.Entities;

    /// <summary>
    /// The CreateSkillCommand class represents a command to create a new skill in the database.
    /// </summary>
    public class CreateSkillCommand : IRequest<Skill>
    {
        /// <summary>
        /// Gets or sets the name of the skill.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the proficiency level of the skill.
        /// </summary>
        public int Proficiency { get; set; }

        /// <summary>
        /// Gets or sets the number of years of experience with this skill.
        /// </summary>
        public int YearsOfExperience { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the resume this skill belongs to.
        /// </summary>
        public Guid ResumeId { get; set; }
    }
}
