// <copyright file="DeleteResumeCommandHandler.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Resume.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using ResumeProject.Infrastructure.Data;

    /// <summary>
    /// The DeleteResumeCommandHandler class handles the command to delete an existing resume from the database.
    /// </summary>
    public class DeleteResumeCommandHandler : IRequestHandler<DeleteResumeCommand, Unit>
    {
        private readonly AppDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteResumeCommandHandler"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public DeleteResumeCommandHandler(AppDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// The Handle method processes the DeleteResumeCommand request and deletes an existing resume from the database.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result.</returns>
        /// <exception cref="KeyNotFoundException">The exception.</exception>
        public async Task<Unit> Handle(DeleteResumeCommand request, CancellationToken cancellationToken)
        {
            var resume = await this.context.Resume.FindAsync(new object[] { request.Id }, cancellationToken);
            if (resume == null)
            {
                throw new KeyNotFoundException("Resume not found.");
            }

            this.context.Resume.Remove(resume);
            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
