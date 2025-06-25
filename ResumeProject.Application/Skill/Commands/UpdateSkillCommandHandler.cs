// <copyright file="UpdateSkillCommandHandler.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Skill.Commands
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using ResumeProject.Infrastructure.Data;

    /// <summary>
    /// The UpdateSkillCommandHandler class handles the command to update an existing skill record in the database.
    /// </summary>
    public class UpdateSkillCommandHandler : IRequestHandler<UpdateSkillCommand, Unit>
    {
        private readonly AppDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSkillCommandHandler"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public UpdateSkillCommandHandler(AppDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// The Handle method processes the UpdateSkillCommand request and updates an existing skill record in the database.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result.</returns>
        /// <exception cref="KeyNotFoundException">The exception.</exception>
        public async Task<Unit> Handle(UpdateSkillCommand request, CancellationToken cancellationToken)
        {
            var skill = await this.context.Skill.FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

            if (skill == null)
            {
                throw new KeyNotFoundException($"Skill with Id {request.Id} not found.");
            }

            skill.Name = request.Name ?? skill.Name;
            skill.Proficiency = request.Proficiency ?? skill.Proficiency;
            skill.YearsOfExperience = request.YearsOfExperience ?? skill.YearsOfExperience;
            skill.ResumeId = request.ResumeId ?? skill.ResumeId;

            this.context.Entry(skill).State = EntityState.Modified;

            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
