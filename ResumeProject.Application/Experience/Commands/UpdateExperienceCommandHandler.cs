// <copyright file="UpdateExperienceCommandHandler.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Experience.Commands
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using ResumeProject.Infrastructure.Data;

    /// <summary>
    /// The UpdateExperienceCommandHandler class handles the command to update an existing experience record in the database.
    /// </summary>
    public class UpdateExperienceCommandHandler : IRequestHandler<UpdateExperienceCommand, Unit>
    {
        private readonly AppDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateExperienceCommandHandler"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public UpdateExperienceCommandHandler(AppDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// The Handle method processes the UpdateExperienceCommand request and updates an existing experience record in the database.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result.</returns>
        /// <exception cref="KeyNotFoundException">The exception.</exception>
        public async Task<Unit> Handle(UpdateExperienceCommand request, CancellationToken cancellationToken)
        {
            var experience = await this.context.Experience.FindAsync(new object[] { request.Id }, cancellationToken);
            if (experience == null)
            {
                throw new KeyNotFoundException($"Experience with ID {request.Id} not found.");
            }

            experience.Position = request.Position ?? experience.Position;
            experience.Company = request.Company ?? experience.Company;
            experience.StartDate = request.StartDate ?? experience.StartDate;
            experience.EndDate = request.EndDate ?? experience.EndDate;
            experience.Description = request.Description ?? experience.Description;
            experience.ResumeId = request.ResumeId ?? experience.ResumeId;

            this.context.Entry(experience).State = EntityState.Modified;

            try
            {
                await this.context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await this.context.Experience.AnyAsync(e => e.Id == request.Id, cancellationToken))
                {
                    throw new KeyNotFoundException($"Experience with ID {request.Id} no longer exists.");
                }

                throw;
            }

            return Unit.Value;
        }
    }
}
