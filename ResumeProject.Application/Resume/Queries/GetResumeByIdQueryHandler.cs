// <copyright file="GetResumeByIdQueryHandler.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Resume.Queries
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using ResumeProject.Domain.Entities;
    using ResumeProject.Infrastructure.Data;

    /// <summary>
    /// The GetResumeByIdQueryHandler class handles the query to retrieve a specific resume record by its unique identifier.
    /// </summary>
    public class GetResumeByIdQueryHandler : IRequestHandler<GetResumeByIdQuery, Resume?>
    {
        private readonly AppDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetResumeByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public GetResumeByIdQueryHandler(AppDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// The Handle method processes the GetResumeByIdQuery request and retrieves a specific resume record from the database by its unique identifier.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The resume entity.</returns>
        public async Task<Resume?> Handle(GetResumeByIdQuery request, CancellationToken cancellationToken)
        {
            return await this.context.Resume
                    .Include(r => r.Experiences)
                    .Include(r => r.Educations)
                    .Include(r => r.Skills)
                    .Include(r => r.Links)
                    .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);
        }
    }
}
