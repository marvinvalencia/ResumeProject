// <copyright file="DeleteExperienceCommandHandler.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Experience.Commands
{
    using MediatR;
    using ResumeProject.Infrastructure.Data;

    /// <summary>
    /// The DeleteExperienceCommandHandler class handles the command to delete an existing experience record from the database.
    /// </summary>
    public class DeleteExperienceCommandHandler : IRequestHandler<DeleteExperienceCommand, Unit>
    {
        private readonly AppDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteExperienceCommandHandler"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public DeleteExperienceCommandHandler(AppDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// The Handle method processes the DeleteExperienceCommand request and deletes an existing experience record from the database.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result.</returns>
        /// <exception cref="KeyNotFoundException">The exception.</exception>
        public async Task<Unit> Handle(DeleteExperienceCommand request, CancellationToken cancellationToken)
        {
            var experience = await this.context.Experience.FindAsync(new object[] { request.Id }, cancellationToken);

            if (experience == null)
            {
                throw new KeyNotFoundException("Experience not found.");
            }

            this.context.Experience.Remove(experience);
            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
