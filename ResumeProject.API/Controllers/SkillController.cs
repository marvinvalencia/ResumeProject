// <copyright file="SkillController.cs" company="marvinvalencia">
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
    /// The SkillController class provides endpoints for managing skills in the application.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SkillController : ControllerBase
    {
        private readonly AppDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="SkillController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public SkillController(AppDbContext context)
        {
            this.context = context;
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
            return await this.context.Skill.ToListAsync();
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
        public async Task<ActionResult<Skill>> Get(Guid id)
        {
            var skill = await this.context.Skill.FindAsync(id);
            if (skill == null)
            {
                return this.NotFound();
            }

            return skill;
        }

        /// <summary>
        /// The Create method adds a new skill to the database.
        /// POST: api/Skill.
        /// </summary>
        /// <param name="skill">The skill.</param>
        /// <returns>The result.</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(Skill), StatusCodes.Status201Created)]
        public async Task<ActionResult<Skill>> Create(Skill skill)
        {
            this.context.Skill.Add(skill);
            await this.context.SaveChangesAsync();

            return this.CreatedAtAction(nameof(this.Get), new { id = skill.Id }, skill);
        }

        /// <summary>
        /// The Update method updates an existing skill in the database.
        /// PUT: api/Skill/5.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <param name="skill">The skill.</param>
        /// <returns>The result.</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, Skill skill)
        {
            if (id != skill.Id)
            {
                return this.BadRequest();
            }

            this.context.Entry(skill).State = EntityState.Modified;

            try
            {
                await this.context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.SkillExists(id))
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
            var skill = await this.context.Skill.FindAsync(id);
            if (skill == null)
            {
                return this.NotFound();
            }

            this.context.Skill.Remove(skill);
            await this.context.SaveChangesAsync();

            return this.NoContent();
        }

        /// <summary>
        /// The SkillExists method checks if a skill with the specified Id exists in the database.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <returns>The boolean.</returns>
        private bool SkillExists(Guid id) =>
            this.context.Skill.Any(e => e.Id == id);
    }
}
