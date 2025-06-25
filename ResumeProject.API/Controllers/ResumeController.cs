// <copyright file="ResumeController.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.API.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ResumeProject.Application.Experience.Queries;
    using ResumeProject.Application.Resume.Commands;
    using ResumeProject.Application.Resume.Queries;
    using ResumeProject.Domain.Entities;

    /// <summary>
    /// The ResumeController class provides endpoints for managing resumes in the application.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class ResumeController : ControllerBase
    {
        private readonly IMediator mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResumeController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        public ResumeController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// The GetAll method retrieves all resumes from the database.
        /// GET: api/resume.
        /// </summary>
        /// <returns>The entities.</returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<Resume>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Resume>>> GetAll()
        {
            var result = await this.mediator.Send(new GetAllResumeQuery());
            return this.Ok(result);
        }

        /// <summary>
        /// The Get method retrieves a specific resume by its unique identifier.
        /// GET: api/resume/5.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <returns>The entity.</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        [ProducesResponseType(typeof(Resume), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Resume>> Get(Guid id)
        {
            var resume = await this.mediator.Send(new GetResumeByIdQuery(id));

            if (resume == null)
            {
                return this.NotFound();
            }

            return this.Ok(resume);
        }

        /// <summary>
        /// The Create method adds a new resume to the database.
        /// POST: api/resume.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>The result.</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(Resume), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateResumeCommand command)
        {
            var resume = await this.mediator.Send(command);
            return this.CreatedAtAction(nameof(this.Get), new { id = resume.Id }, resume);
        }

        /// <summary>
        /// The Update method updates an existing resume in the database.
        /// PUT: api/resume/5.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <param name="command">The command.</param>
        /// <returns>The result.</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateResumeCommand command)
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
        /// The Delete method removes a specific resume from the database.
        /// DELETE: api/resume/5.
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
                await this.mediator.Send(new DeleteResumeCommand(id));
                return this.NoContent();
            }
            catch (KeyNotFoundException)
            {
                return this.NotFound();
            }
        }
    }
}
