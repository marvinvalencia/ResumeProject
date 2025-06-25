// <copyright file="GetAllResumeQuery.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Resume.Queries
{
    using System.Collections.Generic;
    using MediatR;
    using ResumeProject.Domain.Entities;

    /// <summary>
    /// The GetAllResumeQuery class represents a query to retrieve all resume records from the database.
    /// </summary>
    public class GetAllResumeQuery : IRequest<List<Resume>>
    {
    }
}
