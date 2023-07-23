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
    public class CommentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Comments
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Comment>>> Getcomment()
        {
          if (_context.comment == null)
          {
              return NotFound();
          }
            return await _context.comment.ToListAsync();
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int? id)
        {
          if (_context.comment == null)
          {
              return NotFound();
          }
            var comment = await _context.comment.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }
            return comment;
        }

        // PUT: api/Comments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutComment(int? id, Comment comment)
        {
            if (id != comment.commentId)
            {
                return BadRequest();
            }

            _context.Entry(comment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
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

        // POST: api/Comments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize (Roles ="Admin")]
        public async Task<ActionResult<Comment>> PostComment(Comment comment)
        {
          if (_context.comment == null)
          {
              return Problem("Entity set 'ApplicationDbContext.comment'  is null.");
          }
            _context.comment.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComment", new { id = comment.commentId }, comment);
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        [Authorize (Roles ="Admin")]
        public async Task<IActionResult> DeleteComment(int? id)
        {
            if (_context.comment == null)
            {
                return NotFound();
            }
            var comment = await _context.comment.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.comment.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommentExists(int? id)
        {
            return (_context.comment?.Any(e => e.commentId == id)).GetValueOrDefault();
        }
    }
}
