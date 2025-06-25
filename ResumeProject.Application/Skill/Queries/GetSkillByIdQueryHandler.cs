// <copyright file="GetSkillByIdQueryHandler.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Skill.Queries
{
    using MediatR;
    using ResumeProject.Domain.Entities;
    using ResumeProject.Infrastructure.Data;

    /// <summary>
    /// The GetSkillByIdQueryHandler class handles the query to retrieve a specific resume skill by its unique identifier.
    /// </summary>
    public class GetSkillByIdQueryHandler : IRequestHandler<GetSkillByIdQuery, Skill?>
    {
        private readonly AppDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSkillByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public GetSkillByIdQueryHandler(AppDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// The Handle method processes the GetSkillByIdQuery request and retrieves a specific skill record from the database by its unique identifier.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The skill entity.</returns>
        public async Task<Skill?> Handle(GetSkillByIdQuery request, CancellationToken cancellationToken)
        {
            return await this.context.Skill.FindAsync(new object[] { request.Id }, cancellationToken);
        }
    }
}
