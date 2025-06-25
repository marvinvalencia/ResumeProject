// <copyright file="ExperienceController.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.API.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ResumeProject.Application.Experience.Commands;
    using ResumeProject.Application.Experience.Queries;
    using ResumeProject.Domain.Entities;

    /// <summary>
    /// The ExperienceController class provides API endpoints for managing user experiences.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ExperienceController : ControllerBase
    {
        private readonly IMediator mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExperienceController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        public ExperienceController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// The GetAll method retrieves all experiences from the database.
        /// GET: api/Experience.
        /// </summary>
        /// <returns>The experience entities.</returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<Experience>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Experience>>> GetAll()
        {
            var result = await this.mediator.Send(new GetAllExperienceQuery());
            return this.Ok(result);
        }

        /// <summary>
        /// The Get method retrieves a specific experience by its unique identifier.
        /// GET: api/Experience/5.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <returns>The entity.</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        [ProducesResponseType(typeof(Experience), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Experience>> Get(Guid id)
        {
            var experience = await this.mediator.Send(new GetExperienceByIdQuery(id));

            if (experience == null)
            {
                return this.NotFound();
            }

            return this.Ok(experience);
        }

        /// <summary>
        /// The Create method adds a new experience to the database.
        /// POST: api/Education.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>The result.</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(Experience), StatusCodes.Status201Created)]
        public async Task<ActionResult<Experience>> Create([FromBody] CreateExperienceCommand command)
        {
            var result = await this.mediator.Send(command);

            return this.CreatedAtAction(nameof(this.Get), new { id = result.Id }, result);
        }

        /// <summary>
        /// The Update method updates an existing experience in the database.
        /// PUT: api/Education/5.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <param name="command">The command.</param>
        /// <returns>The result.</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateExperienceCommand command)
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
        /// The Delete method removes an experience from the database.
        /// DELETE: api/Education/5.
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
                await this.mediator.Send(new DeleteExperienceCommand(id));
                return this.NoContent();
            }
            catch (KeyNotFoundException)
            {
                return this.NotFound();
            }
        }
    }
}
