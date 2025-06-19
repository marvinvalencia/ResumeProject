using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResumeProject.Domain.Entities;
using ResumeProject.Infrastructure.Data;

namespace ResumeProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class ExperienceController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ExperienceController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Education
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Experience>>> GetAll()
        {
            return await _context.Experience.ToListAsync();
        }

        // GET: api/Education/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<Experience>> Get(Guid id)
        {
            var experience = await _context.Experience.FindAsync(id);
            if (experience == null)
                return NotFound();

            return experience;
        }

        // POST: api/Education
        [HttpPost]
        public async Task<ActionResult<Experience>> Create(Experience experience)
        {
            _context.Experience.Add(experience);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = experience.Id }, experience);
        }

        // PUT: api/Education/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Experience experience)
        {
            if (id != experience.Id)
                return BadRequest();

            _context.Entry(experience).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExperienceExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/Education/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var experience = await _context.Experience.FindAsync(id);
            if (experience == null)
                return NotFound();

            _context.Experience.Remove(experience);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExperienceExists(Guid id) =>
            _context.Experience.Any(e => e.Id == id);
    }
}
