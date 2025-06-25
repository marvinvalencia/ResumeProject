// <copyright file="GetAllEducationQuery.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Education.Queries
{
    using System.Collections.Generic;
    using MediatR;
    using ResumeProject.Domain.Entities;

    /// <summary>
    /// The GetAllEducationQuery class represents a query to retrieve all education records from the database.
    /// </summary>
    public class GetAllEducationQuery : IRequest<List<Education>>
    {
    }
}
