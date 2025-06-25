// <copyright file="CreateEducationCommandHandler.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Education.Commands
{
    using MediatR;
    using ResumeProject.Domain.Entities;
    using ResumeProject.Infrastructure.Data;

    /// <summary>
    /// The CreateEducationCommandHandler class handles the command to create a new education record in the database.
    /// </summary>
    public class CreateEducationCommandHandler : IRequestHandler<CreateEducationCommand, Education>
    {
        private readonly AppDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateEducationCommandHandler"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public CreateEducationCommandHandler(AppDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// The Handle method processes the CreateEducationCommand request and creates a new education record in the database.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The education entity.</returns>
        public async Task<Education> Handle(CreateEducationCommand request, CancellationToken cancellationToken)
        {
            var education = new Education
            {
                Degree = request.Degree,
                Institution = request.Institution,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Major = request.Major,
                Description = request.Description ?? string.Empty,
                GPA = request.GPA ?? 0,
                ResumeId = request.ResumeId,
            };

            this.context.Education.Add(education);
            await this.context.SaveChangesAsync(cancellationToken);

            return education;
        }
    }
}
