//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MarketplaceApp.Data;
using MarketplaceApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace MarketplaceApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
    public class SolutionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SolutionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Solutions
        [HttpGet]
        [Authorize (Roles ="Admin")]
        public async Task<ActionResult<IEnumerable<Solution>>> Getsolution()
        {
            if (_context.solution == null)
            {
                return NotFound();
            }
            return await _context.solution.ToListAsync();
        }

        //GET: Solutions/questId
        [HttpGet("[controller]/questId={questId}")]
        public async Task<ActionResult<IEnumerable<Solution>>> GetSolutionsByQuestId(int? questId)
        {
            if (_context.solution == null)
            {
                return NotFound();
            }
            return await _context.solution.Where(s => s.questId == questId).ToListAsync();
        }

        // GET: api/Solutions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Solution>> GetSolution(int? id)
        {
          if (_context.solution == null)
          {
              return NotFound();
          }
            //var solution = await _context.solution.FindAsync(id);
            var solution = await _context.solution.Include(s => s.comments).FirstOrDefaultAsync(s => s.solutionId == id);

            if (solution == null)
            {
                return NotFound();
            }

            return solution;
        }

        // PUT: api/Solutions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSolution(int? id, Solution solution)
        {
            if (id != solution.solutionId)
            {
                return BadRequest();
            }
            ///!!!
            var _quest = await _context.quest.FindAsync(solution.questId);
            if(_quest.isSolved == true && _quest != null)
            {
                if (solution.isSelected == false)
                {
                    var _solutions = await _context.solution.Where(s => s.questId == _quest.questId && s.isSelected == true && s.solutionId != solution.solutionId).ToListAsync();
                    if (_solutions.Count == 0)
                    {
                        _quest.isSolved = false;
                        _context.Entry(_quest).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }
                }
            }
            else if (_quest.isSolved == false && _quest != null && solution.isSelected == true) {
                _quest.isSolved = true;
                _context.Entry(_quest).State=EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            ///!!!

            _context.Entry(solution).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SolutionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Solutions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Solution>> PostSolution(Solution solution)
        {
          if (_context.solution == null)
          {
              return Problem("Entity set 'ApplicationDbContext.solution'  is null.");
          }
            _context.solution.Add(solution);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSolution", new { id = solution.solutionId }, solution);
        }

        // DELETE: api/Solutions/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSolution(int? id)
        {
            if (_context.solution == null)
            {
                return NotFound();
            }
            var solution = await _context.solution.FindAsync(id);
            if (solution == null)
            {
                return NotFound();
            }

            _context.solution.Remove(solution);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SolutionExists(int? id)
        {
            return (_context.solution?.Any(e => e.solutionId == id)).GetValueOrDefault();
        }
    }
}
