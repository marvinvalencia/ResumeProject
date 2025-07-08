// <copyright file="SkillController.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.API.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ResumeProject.Application.Skill.Commands;
    using ResumeProject.Application.Skill.Queries;
    using ResumeProject.Domain.Entities;

    /// <summary>
    /// The SkillController class provides endpoints for managing skills in the application.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SkillController : BaseApiController
    {
        private readonly IMediator mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="SkillController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        public SkillController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// The GetAll method retrieves all skills from the database.
        /// GET: api/Skill.
        /// </summary>
        /// <returns>The entities.</returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<Skill>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Skill>>> GetAll()
        {
            var result = await this.mediator.Send(new GetAllSkillQuery());
            return this.Ok(result);
        }

        /// <summary>
        /// The Get method retrieves a single skill by its unique identifier.
        /// GET: api/Skill/5.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <returns>The entity.</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        [ProducesResponseType(typeof(Skill), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Skill>> Get(Guid id)
        {
            var resume = await this.mediator.Send(new GetSkillByIdQuery(id));

            if (resume == null)
            {
                return this.NotFound();
            }

            return this.Ok(resume);
        }

        /// <summary>
        /// The Create method adds a new skill to the database.
        /// POST: api/Skill.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>The result.</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(Skill), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateSkillCommand command)
        {
            var skill = await this.mediator.Send(command);
            return this.CreatedAtAction(nameof(this.Get), new { id = skill.Id }, skill);
        }

        /// <summary>
        /// The Update method updates an existing skill in the database.
        /// PUT: api/Skill/5.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <param name="command">The command.</param>
        /// <returns>The result.</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSkillCommand command)
        {
            if (id != command.Id)
            {
                return this.BadRequest("Mismatched Experience ID.");
            }

            try
            {
                await this.mediator.Send(command);
            }
            catch (KeyNotFoundException)
            {
                return this.NotFound();
            }

            return this.NoContent();
        }

        /// <summary>
        /// The Delete method removes a skill from the database.
        /// DELETE: api/Skill/5.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <returns>The result.</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await this.mediator.Send(new DeleteSkillCommand(id));
                return this.NoContent();
            }
            catch (KeyNotFoundException)
            {
                return this.NotFound();
            }
        }
    }
}
