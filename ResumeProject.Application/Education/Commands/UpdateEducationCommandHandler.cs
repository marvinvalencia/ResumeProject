// <copyright file="UpdateEducationCommandHandler.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Education.Commands
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using ResumeProject.Infrastructure.Data;

    /// <summary>
    /// The UpdateEducationCommandHandler class handles the command to update an existing education record in the database.
    /// </summary>
    public class UpdateEducationCommandHandler : IRequestHandler<UpdateEducationCommand, Unit>
    {
        private readonly AppDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateEducationCommandHandler"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public UpdateEducationCommandHandler(AppDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// The Handle method processes the UpdateEducationCommand request and updates an existing education record in the database.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The education entity.</returns>
        /// <exception cref="KeyNotFoundException">The exception.</exception>
        public async Task<Unit> Handle(UpdateEducationCommand request, CancellationToken cancellationToken)
        {
            var education = await this.context.Education.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            if (education == null)
            {
                throw new KeyNotFoundException("Education record not found.");
            }

            education.Degree = request.Degree ?? education.Degree;
            education.Institution = request.Institution ?? education.Institution;
            education.StartDate = request.StartDate ?? education.StartDate;
            education.EndDate = request.EndDate ?? education.EndDate;
            education.Major = request.Major ?? education.Major;
            education.Description = request.Description ?? education.Description;
            education.GPA = request.GPA ?? education.GPA;
            education.ResumeId = request.ResumeId ?? education.ResumeId;

            this.context.Entry(education).State = EntityState.Modified;

            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
