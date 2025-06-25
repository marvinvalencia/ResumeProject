// <copyright file="UpdateResumeCommandHandler.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Resume.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using ResumeProject.Infrastructure.Data;

    /// <summary>
    /// The UpdateResumeCommandHandler class handles the command to update an existing resume in the database.
    /// </summary>
    public class UpdateResumeCommandHandler : IRequestHandler<UpdateResumeCommand, Unit>
    {
        private readonly AppDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateResumeCommandHandler"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public UpdateResumeCommandHandler(AppDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// The Handle method processes the UpdateResumeCommand request and updates an existing resume in the database.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result.</returns>
        /// <exception cref="KeyNotFoundException">The exception.</exception>
        public async Task<Unit> Handle(UpdateResumeCommand request, CancellationToken cancellationToken)
        {
            var resume = await this.context.Resume.FindAsync(request.Id);
            if (resume == null)
            {
                throw new KeyNotFoundException("Resume not found.");
            }

            if (request.FirstName != null && request.LastName != null)
            {
                resume.Name = string.Format("{0} {1}", request.FirstName, request.LastName);
            }

            resume.Id = request.Id;
            resume.Picture = request.Picture ?? resume.Picture;
            resume.Email = request.Email ?? resume.Email;
            resume.PhoneNumber = request.PhoneNumber ?? resume.PhoneNumber;
            resume.Address = request.Address ?? resume.Address;
            resume.Summary = request.Summary ?? resume.Summary;
            resume.Interests = request.Interests ?? resume.Interests;

            this.context.Entry(resume).State = EntityState.Modified;

            try
            {
                await this.context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = await this.context.Resume.AnyAsync(r => r.Id == request.Id, cancellationToken);
                if (!exists)
                {
                    throw new KeyNotFoundException("Resume not found during concurrency check.");
                }

                throw; // Re-throw if it's a real concurrency problem
            }

            return Unit.Value;
        }
    }
}
