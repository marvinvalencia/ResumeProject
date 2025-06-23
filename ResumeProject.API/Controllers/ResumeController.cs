// <copyright file="ResumeController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ResumeProject.API.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using ResumeProject.Domain.Entities;
    using ResumeProject.Infrastructure.Data;

    /// <summary>
    /// The ResumeController class provides endpoints for managing resumes in the application.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class ResumeController : ControllerBase
    {
        private readonly AppDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResumeController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ResumeController(AppDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// The GetAll method retrieves all resumes from the database.
        /// GET: api/resume.
        /// </summary>
        /// <returns>The entities.</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Resume>>> GetAll()
        {
            return await this.context.Resume.ToListAsync();
        }

        /// <summary>
        /// The Get method retrieves a specific resume by its unique identifier.
        /// GET: api/resume/5.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <returns>The entity.</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<Resume>> Get(Guid id)
        {
            var resume = await this.context.Resume.FindAsync(id);
            if (resume == null)
            {
                return this.NotFound();
            }

            return resume;
        }

        /// <summary>
        /// The Create method adds a new resume to the database.
        /// POST: api/resume.
        /// </summary>
        /// <param name="resume">The resume.</param>
        /// <returns>The result.</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Resume>> Create(Resume resume)
        {
            this.context.Resume.Add(resume);
            await this.context.SaveChangesAsync();

            return this.CreatedAtAction(nameof(this.Get), new { id = resume.Id }, resume);
        }

        /// <summary>
        /// The Update method updates an existing resume in the database.
        /// PUT: api/resume/5.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <param name="resume">The resume.</param>
        /// <returns>The result.</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, Resume resume)
        {
            if (id != resume.Id)
            {
                return this.BadRequest();
            }

            this.context.Entry(resume).State = EntityState.Modified;

            try
            {
                await this.context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.ResumeExists(id))
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
        /// The Delete method removes a specific resume from the database.
        /// DELETE: api/resume/5.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <returns>The result.</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var resume = await this.context.Resume.FindAsync(id);
            if (resume == null)
            {
                return this.NotFound();
            }

            this.context.Resume.Remove(resume);
            await this.context.SaveChangesAsync();

            return this.NoContent();
        }

        /// <summary>
        /// The ResumeExists method checks if a resume with the specified Id exists in the database.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <returns>The boolean.</returns>
        private bool ResumeExists(Guid id) =>
            this.context.Resume.Any(e => e.Id == id);
    }
}
