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
    public class SkillController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SkillController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Skill
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Skill>>> GetAll()
        {
            return await _context.Skill.ToListAsync();
        }

        // GET: api/Skill/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<Skill>> Get(Guid id)
        {
            var skill = await _context.Skill.FindAsync(id);
            if (skill == null)
                return NotFound();

            return skill;
        }

        // POST: api/Skill
        [HttpPost]
        public async Task<ActionResult<Skill>> Create(Skill skill)
        {
            _context.Skill.Add(skill);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = skill.Id }, skill);
        }

        // PUT: api/Skill/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Skill skill)
        {
            if (id != skill.Id)
                return BadRequest();

            _context.Entry(skill).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkillExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/Skill/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var skill = await _context.Skill.FindAsync(id);
            if (skill == null)
                return NotFound();

            _context.Skill.Remove(skill);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SkillExists(Guid id) =>
            _context.Skill.Any(e => e.Id == id);
    }
}
