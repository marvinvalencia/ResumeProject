// <copyright file="CreateSkillCommandHandler.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Skill.Commands
{
    using MediatR;
    using ResumeProject.Domain.Entities;
    using ResumeProject.Infrastructure.Data;

    /// <summary>
    /// The CreateSkillCommandHandler class handles the command to create a new skill in the database.
    /// </summary>
    public class CreateSkillCommandHandler : IRequestHandler<CreateSkillCommand, Skill>
    {
        private readonly AppDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSkillCommandHandler"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public CreateSkillCommandHandler(AppDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// The Handle method processes the CreateSkillCommand request and creates a new skill in the database.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The created skill entity.</returns>
        public async Task<Skill> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
        {
            var skill = new Skill
            {
                Name = request.Name,
                Proficiency = request.Proficiency,
                YearsOfExperience = request.YearsOfExperience,
                ResumeId = request.ResumeId,
            };

            this.context.Skill.Add(skill);
            await this.context.SaveChangesAsync(cancellationToken);
            return skill;
        }
    }
}
