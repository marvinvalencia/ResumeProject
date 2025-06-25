// <copyright file="DeleteSkillCommandHandler.cs" company="marvinvalencia">
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
    /// The DeleteSkillCommandHandler class handles the command to delete an existing skill from the database.
    /// </summary>
    public class DeleteSkillCommandHandler : IRequestHandler<DeleteSkillCommand, Unit>
    {
        private readonly AppDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteSkillCommandHandler"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public DeleteSkillCommandHandler(AppDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// The Handle method processes the DeleteSkillCommand request and deletes an existing skill from the database.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result.</returns>
        /// <exception cref="KeyNotFoundException">The exception.</exception>
        public async Task<Unit> Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
        {
            var skill = await this.context.Skill.FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

            if (skill == null)
            {
                throw new KeyNotFoundException($"Skill with Id {request.Id} not found.");
            }

            this.context.Skill.Remove(skill);
            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
