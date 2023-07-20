using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MarketplaceApp.Data;
using MarketplaceApp.Models;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace MarketplaceApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
    public class QuestsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public QuestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Quests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quest>>> Getquest()
        {
          if (_context.quest == null)
          {
              return NotFound();
          }
            return await _context.quest.ToListAsync();
            //return await _context.quest.Include(q => q.user).ToListAsync();

        }

        // GET: api/Quests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Quest>> GetQuest(int? id)
        {
          if (_context.quest == null)
          {
              return NotFound();
          }
            //var quest = await _context.quest.Include(q => q.solutions).FirstOrDefaultAsync(q => q.questId == id);
            var quest = await _context.quest.FindAsync(id);

            if (quest == null)
            {
                return NotFound();
            }

            var user = await _context.user.FindAsync(quest.userId);
            quest.user = user;

            return quest;
        }

        // PUT: api/Quests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutQuest(int? id, Quest quest)
        {
            if (id != quest.questId)
            {
                return BadRequest();
            }

            _context.Entry(quest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestExists(id))
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

        // POST: api/Quests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Quest>> PostQuest(Quest quest)
        {
          if (_context.quest == null)
          {
              return Problem("Entity set 'ApplicationDbContext.quest'  is null.");
          }
            quest.endDate = (quest.endDate < DateTime.Now.AddDays(-1) | quest.endDate == null) ? DateTime.Now.AddDays(7) : quest.endDate;
            _context.quest.Add(quest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuest", new { id = quest.questId }, quest);
        }

        // DELETE: api/Quests/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteQuest(int? id)
        {
            if (_context.quest == null)
            {
                return NotFound();
            }
            var quest = await _context.quest.FindAsync(id);
            if (quest == null)
            {
                return NotFound();
            }

            _context.quest.Remove(quest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestExists(int? id)
        {
            return (_context.quest?.Any(e => e.questId == id)).GetValueOrDefault();
        }
    }
}
