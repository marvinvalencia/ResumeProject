// <copyright file="GetExperienceByIdQueryHandler.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Experience.Queries
{
    using MediatR;
    using ResumeProject.Domain.Entities;
    using ResumeProject.Infrastructure.Data;

    /// <summary>
    /// The GetEducationByIdQueryHandler class handles the query to retrieve a specific experience record by its unique identifier.
    /// </summary>
    public class GetExperienceByIdQueryHandler : IRequestHandler<GetExperienceByIdQuery, Experience?>
    {
        private readonly AppDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetExperienceByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public GetExperienceByIdQueryHandler(AppDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// The Handle method processes the GetExperienceByIdQuery request and retrieves a specific experience record from the database by its unique identifier.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The experience entity.</returns>
        public async Task<Experience?> Handle(GetExperienceByIdQuery request, CancellationToken cancellationToken)
        {
            return await this.context.Experience.FindAsync(new object[] { request.Id }, cancellationToken);
        }
    }
}
