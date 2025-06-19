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
    public class EducationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EducationController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Education
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Education>>> GetAll()
        {
            return await _context.Education.ToListAsync();
        }

        // GET: api/Education/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<Education>> Get(Guid id)
        {
            var education = await _context.Education.FindAsync(id);
            if (education == null)
                return NotFound();

            return education;
        }

        // POST: api/Education
        [HttpPost]
        public async Task<ActionResult<Education>> Create(Education education)
        {
            _context.Education.Add(education);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = education.Id }, education);
        }

        // PUT: api/Education/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Education education)
        {
            if (id != education.Id)
                return BadRequest();

            _context.Entry(education).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EducationExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/education/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var education = await _context.Education.FindAsync(id);
            if (education == null)
                return NotFound();

            _context.Education.Remove(education);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EducationExists(Guid id) =>
            _context.Education.Any(e => e.Id == id);
    }
}
