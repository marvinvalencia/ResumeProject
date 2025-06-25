// <copyright file="GetEducationByIdQueryHandler.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Education.Queries
{
    using MediatR;
    using ResumeProject.Domain.Entities;
    using ResumeProject.Infrastructure.Data;

    /// <summary>
    /// The GetEducationByIdQueryHandler class handles the query to retrieve a specific education record by its unique identifier.
    /// </summary>
    public class GetEducationByIdQueryHandler : IRequestHandler<GetEducationByIdQuery, Education?>
    {
        private readonly AppDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetEducationByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public GetEducationByIdQueryHandler(AppDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// The Handle method processes the GetEducationByIdQuery request and retrieves a specific education record from the database by its unique identifier.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The education entity.</returns>
        public async Task<Education?> Handle(GetEducationByIdQuery request, CancellationToken cancellationToken)
        {
            return await this.context.Education.FindAsync(new object[] { request.Id }, cancellationToken);
        }
    }
}
