// <copyright file="GetAllExperienceQuery.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Experience.Queries
{
    using System.Collections.Generic;
    using MediatR;
    using ResumeProject.Domain.Entities;

    /// <summary>
    /// The GetAllExperienceQuery class represents a query to retrieve all experience records from the database.
    /// </summary>
    public class GetAllExperienceQuery : IRequest<List<Experience>>
    {
    }
}
