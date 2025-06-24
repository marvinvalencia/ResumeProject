// <copyright file="EducationController.cs" company="marvinvalencia">
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
    /// The EducationController class provides endpoints for managing education records in the application.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class EducationController : ControllerBase
    {
        private readonly AppDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="EducationController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public EducationController(AppDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// The GetAll method retrieves all education records from the database.
        /// GET: api/Education.
        /// </summary>
        /// <returns>The entities.</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Education>>> GetAll()
        {
            return await this.context.Education.ToListAsync();
        }

        /// <summary>
        /// The Get method retrieves a specific education record by its unique identifier.
        /// GET: api/Education/5.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <returns>The entity.</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<Education>> Get(Guid id)
        {
            var education = await this.context.Education.FindAsync(id);
            if (education == null)
            {
                return this.NotFound();
            }

            return education;
        }

        /// <summary>
        /// The Create method adds a new education record to the database.
        /// POST: api/Education.
        /// </summary>
        /// <param name="education">The education.</param>
        /// <returns>The result.</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Education>> Create(Education education)
        {
            this.context.Education.Add(education);
            await this.context.SaveChangesAsync();

            return this.CreatedAtAction(nameof(this.Get), new { id = education.Id }, education);
        }

        /// <summary>
        /// The Update method updates an existing education record in the database.
        /// PUT: api/Education/5.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <param name="education">The Education.</param>
        /// <returns>The result.</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, Education education)
        {
            if (id != education.Id)
            {
                return this.BadRequest();
            }

            this.context.Entry(education).State = EntityState.Modified;

            try
            {
                await this.context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.EducationExists(id))
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
        /// The Delete method removes an education record from the database.
        /// DELETE: api/education/5.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <returns>The result.</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var education = await this.context.Education.FindAsync(id);
            if (education == null)
            {
                return this.NotFound();
            }

            this.context.Education.Remove(education);
            await this.context.SaveChangesAsync();

            return this.NoContent();
        }

        /// <summary>
        /// The EducationExists method checks if an education record exists in the database by its unique identifier.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <returns>The result.</returns>
        private bool EducationExists(Guid id) =>
            this.context.Education.Any(e => e.Id == id);
    }
}
