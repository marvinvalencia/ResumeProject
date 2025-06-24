// <copyright file="ExperienceController.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.API.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using ResumeProject.Domain.Entities;
    using ResumeProject.Infrastructure.Data;

    /// <summary>
    /// The ExperienceController class provides API endpoints for managing user experiences.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ExperienceController : ControllerBase
    {
        private readonly AppDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExperienceController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ExperienceController(AppDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// The GetAll method retrieves all experiences from the database.
        /// GET: api/Education.
        /// </summary>
        /// <returns>The experience entities.</returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<Experience>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Experience>>> GetAll()
        {
            return await this.context.Experience.ToListAsync();
        }

        /// <summary>
        /// The Get method retrieves a specific experience by its unique identifier.
        /// GET: api/Education/5.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <returns>The entity.</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        [ProducesResponseType(typeof(Experience), StatusCodes.Status200OK)]
        public async Task<ActionResult<Experience>> Get(Guid id)
        {
            var experience = await this.context.Experience.FindAsync(id);
            if (experience == null)
            {
                return this.NotFound();
            }

            return experience;
        }

        /// <summary>
        /// The Create method adds a new experience to the database.
        /// POST: api/Education.
        /// </summary>
        /// <param name="experience">The experience.</param>
        /// <returns>The result.</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(Experience), StatusCodes.Status201Created)]
        public async Task<ActionResult<Experience>> Create(Experience experience)
        {
            this.context.Experience.Add(experience);
            await this.context.SaveChangesAsync();

            return this.CreatedAtAction(nameof(this.Get), new { id = experience.Id }, experience);
        }

        /// <summary>
        /// The Update method updates an existing experience in the database.
        /// PUT: api/Education/5.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <param name="experience">The experience.</param>
        /// <returns>The result.</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, Experience experience)
        {
            if (id != experience.Id)
            {
                return this.BadRequest();
            }

            this.context.Entry(experience).State = EntityState.Modified;

            try
            {
                await this.context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.ExperienceExists(id))
                {
                    return this.NotFound();
                }
                else
                {
                    throw;
                }
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
            var experience = await this.context.Experience.FindAsync(id);
            if (experience == null)
            {
                return this.NotFound();
            }

            this.context.Experience.Remove(experience);
            await this.context.SaveChangesAsync();

            return this.NoContent();
        }

        /// <summary>
        /// The experience exists method checks if an experience with the specified Id exists in the database.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <returns>The boolean.</returns>
        private bool ExperienceExists(Guid id) =>
            this.context.Experience.Any(e => e.Id == id);
    }
}
