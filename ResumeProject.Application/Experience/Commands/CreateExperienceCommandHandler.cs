// <copyright file="CreateExperienceCommandHandler.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Experience.Commands
{
    using MediatR;
    using ResumeProject.Domain.Entities;
    using ResumeProject.Infrastructure.Data;

    /// <summary>
    /// The CreateExperienceCommandHandler class handles the command to create a new experience record in the database.
    /// </summary>
    public class CreateExperienceCommandHandler : IRequestHandler<CreateExperienceCommand, Experience>
    {
        private readonly AppDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateExperienceCommandHandler"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public CreateExperienceCommandHandler(AppDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// The Handle method processes the CreateExperienceCommand request and creates a new experience record in the database.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The experience.</returns>
        public async Task<Experience> Handle(CreateExperienceCommand request, CancellationToken cancellationToken)
        {
            var experience = new Experience
            {
                Position = request.Position,
                Company = request.Company,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Description = request.Description,
                ResumeId = request.ResumeId,
            };

            this.context.Experience.Add(experience);
            await this.context.SaveChangesAsync(cancellationToken);

            return experience;
        }
    }
}
