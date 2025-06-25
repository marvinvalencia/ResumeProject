// <copyright file="GetAllSkillQueryHandler.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Skill.Queries
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
    public class GetAllSkillQueryHandler : IRequestHandler<GetAllSkillQuery, List<Skill>>
    {
        private readonly AppDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllSkillQueryHandler"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public GetAllSkillQueryHandler(AppDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// The Handle method processes the GetAllResumeQuery request and retrieves all education records from the database.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The education entities.</returns>
        public async Task<List<Skill>> Handle(GetAllSkillQuery request, CancellationToken cancellationToken)
        {
            return await this.context.Skill.ToListAsync(cancellationToken);
        }
    }
}
