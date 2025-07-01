// <copyright file="CreateResumeCommandHandler.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Resume.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using ResumeProject.Domain.Entities;
    using ResumeProject.Infrastructure.Data;

    /// <summary>
    /// The CreateResumeCommandHandler class handles the command to create a new resume in the database.
    /// </summary>
    public class CreateResumeCommandHandler : IRequestHandler<CreateResumeCommand, Resume>
    {
        private readonly AppDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateResumeCommandHandler"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public CreateResumeCommandHandler(AppDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// The Handle method processes the CreateResumeCommand request and creates a new resume in the database.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The resume entity.</returns>
        public async Task<Resume> Handle(CreateResumeCommand request, CancellationToken cancellationToken)
        {
            var resume = new Resume
            {
                Picture = request.Picture,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                Summary = request.Summary,
                Interests = request.Interests,
            };

            this.context.Resume.Add(resume);
            await this.context.SaveChangesAsync(cancellationToken);

            return resume;
        }
    }
}
