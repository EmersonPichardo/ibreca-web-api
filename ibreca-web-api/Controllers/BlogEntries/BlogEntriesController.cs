using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ibreca_data_access.Contexts.IbrecaDB;
using ibreca_data_access.Contexts.IbrecaDB.Models;

namespace ibreca_web_api.Controllers.BlogEntries
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogEntriesController : ControllerBase
    {
        private readonly IbrecaDBContext _context;

        public BlogEntriesController(IbrecaDBContext context)
        {
            _context = context;
        }

        // GET: api/BlogEntries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogEntry>>> GetBlogEntries()
        {
            return await _context.BlogEntries.ToListAsync();
        }

        // GET: api/BlogEntries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogEntry>> GetBlogEntry(int id)
        {
            var blogEntry = await _context.BlogEntries.FindAsync(id);

            if (blogEntry == null)
            {
                return NotFound();
            }

            return blogEntry;
        }

        // PUT: api/BlogEntries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlogEntry(int id, BlogEntry blogEntry)
        {
            if (id != blogEntry.Id)
            {
                return BadRequest();
            }

            _context.Entry(blogEntry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogEntryExists(id))
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

        // POST: api/BlogEntries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BlogEntry>> PostBlogEntry(BlogEntry blogEntry)
        {
            _context.BlogEntries.Add(blogEntry);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBlogEntry", new { id = blogEntry.Id }, blogEntry);
        }

        // DELETE: api/BlogEntries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogEntry(int id)
        {
            var blogEntry = await _context.BlogEntries.FindAsync(id);
            if (blogEntry == null)
            {
                return NotFound();
            }

            _context.BlogEntries.Remove(blogEntry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BlogEntryExists(int id)
        {
            return _context.BlogEntries.Any(e => e.Id == id);
        }
    }
}
