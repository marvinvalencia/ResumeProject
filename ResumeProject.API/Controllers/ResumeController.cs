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
    public class ResumeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ResumeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/resume
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Resume>>> GetAll()
        {
            return await _context.Resume.ToListAsync();
        }

        // GET: api/resume/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<Resume>> Get(Guid id)
        {
            var resume = await _context.Resume.FindAsync(id);
            if (resume == null)
                return NotFound();

            return resume;
        }

        // POST: api/resume
        [HttpPost]
        public async Task<ActionResult<Resume>> Create(Resume resume)
        {
            _context.Resume.Add(resume);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = resume.Id }, resume);
        }

        // PUT: api/resume/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Resume resume)
        {
            if (id != resume.Id)
                return BadRequest();

            _context.Entry(resume).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResumeExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/resume/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var resume = await _context.Resume.FindAsync(id);
            if (resume == null)
                return NotFound();

            _context.Resume.Remove(resume);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ResumeExists(Guid id) =>
            _context.Resume.Any(e => e.Id == id);
    }
}
