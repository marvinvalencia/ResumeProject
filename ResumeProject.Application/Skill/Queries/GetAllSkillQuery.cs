// <copyright file="GetAllSkillQuery.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Skill.Queries
{
    using System.Collections.Generic;
    using MediatR;
    using ResumeProject.Domain.Entities;

    /// <summary>
    /// The GetAllSkillQuery class represents a query to retrieve all skill records from the database.
    /// </summary>
    public class GetAllSkillQuery : IRequest<List<Skill>>
    {
    }
}
