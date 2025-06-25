// <copyright file="EducationController.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.API.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ResumeProject.Application.Education.Commands;
    using ResumeProject.Application.Education.Queries;
    using ResumeProject.Domain.Entities;

    /// <summary>
    /// The EducationController class provides endpoints for managing education records in the application.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class EducationController : ControllerBase
    {
        private readonly IMediator mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="EducationController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        public EducationController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// The GetAll method retrieves all education records from the database.
        /// GET: api/Education.
        /// </summary>
        /// <returns>The entities.</returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<Education>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Education>>> GetAll()
        {
            var result = await this.mediator.Send(new GetAllEducationQuery());
            return this.Ok(result);
        }

        /// <summary>
        /// The Get method retrieves a specific education record by its unique identifier.
        /// GET: api/Education/5.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <returns>The entity.</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        [ProducesResponseType(typeof(Education), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Education>> Get(Guid id)
        {
            var education = await this.mediator.Send(new GetEducationByIdQuery(id));

            if (education == null)
            {
                return this.NotFound();
            }

            return this.Ok(education);
        }

        /// <summary>
        /// The Create method adds a new education record to the database.
        /// POST: api/Education.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>The result.</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(Education), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateEducationCommand command)
        {
            var createdEducation = await this.mediator.Send(command);

            return this.CreatedAtAction(nameof(this.Get), new { id = createdEducation.Id }, createdEducation);
        }

        /// <summary>
        /// The Update method updates an existing education record in the database.
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
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEducationCommand command)
        {
            if (id != command.Id)
            {
                return this.BadRequest("Mismatched Education ID.");
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
        /// The Delete method removes an education record from the database.
        /// DELETE: api/education/5.
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
                await this.mediator.Send(new DeleteEducationCommand(id));
                return this.NoContent();
            }
            catch (KeyNotFoundException)
            {
                return this.NotFound();
            }
        }
    }
}
