// <copyright file="GetAllEducationQueryHandler.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Education.Queries
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using ResumeProject.Domain.Entities;
    using ResumeProject.Infrastructure.Data;

    /// <summary>
    /// The GetAllEducationsQueryHandler class handles the query to retrieve all education records from the database.
    /// </summary>
    public class GetAllEducationQueryHandler : IRequestHandler<GetAllEducationQuery, List<Education>>
    {
        private readonly AppDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllEducationQueryHandler"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public GetAllEducationQueryHandler(AppDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// The Handle method processes the GetAllEducationQuery request and retrieves all education records from the database.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The education entities.</returns>
        public async Task<List<Education>> Handle(GetAllEducationQuery request, CancellationToken cancellationToken)
        {
            return await this.context.Education.ToListAsync(cancellationToken);
        }
    }
}
