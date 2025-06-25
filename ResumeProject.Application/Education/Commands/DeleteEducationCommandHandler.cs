// <copyright file="DeleteEducationCommandHandler.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Education.Commands
{
    using MediatR;
    using ResumeProject.Infrastructure.Data;

    /// <summary>
    /// The DeleteEducationCommandHandler class handles the command to delete an existing education record from the database.
    /// </summary>
    public class DeleteEducationCommandHandler : IRequestHandler<DeleteEducationCommand, Unit>
    {
        private readonly AppDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteEducationCommandHandler"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public DeleteEducationCommandHandler(AppDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// The Handle method processes the DeleteEducationCommand request and deletes an existing education record from the database.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result.</returns>
        /// <exception cref="KeyNotFoundException">The exception.</exception>
        public async Task<Unit> Handle(DeleteEducationCommand request, CancellationToken cancellationToken)
        {
            var education = await this.context.Education.FindAsync(new object[] { request.Id }, cancellationToken);

            if (education == null)
            {
                throw new KeyNotFoundException($"Education with ID {request.Id} not found.");
            }

            this.context.Education.Remove(education);
            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
