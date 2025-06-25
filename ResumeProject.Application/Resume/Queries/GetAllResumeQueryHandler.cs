// <copyright file="GetAllResumeQueryHandler.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Resume.Queries
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using ResumeProject.Domain.Entities;
    using ResumeProject.Infrastructure.Data;

    /// <summary>
    /// The GetAllResumeQueryHandler class handles the query to retrieve all experience records from the database.
    /// </summary>
    public class GetAllResumeQueryHandler : IRequestHandler<GetAllResumeQuery, List<Resume>>
    {
        private readonly AppDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllResumeQueryHandler"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public GetAllResumeQueryHandler(AppDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// The Handle method processes the GetAllResumeQuery request and retrieves all education records from the database.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The education entities.</returns>
        public async Task<List<Resume>> Handle(GetAllResumeQuery request, CancellationToken cancellationToken)
        {
            return await this.context.Resume
                    .Include(r => r.Experiences)
                    .Include(r => r.Educations)
                    .Include(r => r.Skills)
                    .ToListAsync(cancellationToken);
        }
    }
}
