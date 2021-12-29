using ibreca_data_access.Contexts.IbrecaDB;
using ibreca_data_access.Contexts.IbrecaDB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ibreca_web_api.Controllers.BlogEntries
{
    public class BlogEntriesController : AuthControllerBase
    {
        private readonly IbrecaDBContext _context;

        public BlogEntriesController(IbrecaDBContext context)
        {
            _context = context;
        }

        [HttpGet("page/{page}/{search}/{from}/{to}")]
        public async Task<ActionResult<Page<BlogEntryDto>>> GetBlogEntriesPage(int page, string search = null, DateTime? from = null, DateTime? to = null)
        {
            List<BlogEntry> list =
                await _context.BlogEntries
                    .Where(entry =>
                        (string.IsNullOrWhiteSpace(search) ? true : entry.Title.Contains(search)) &&
                        (from.HasValue ? (entry.PublicationDate >= from.Value) : true) &&
                        (to.HasValue ? (entry.PublicationDate <= to.Value) : true)
                    ).ToListAsync();

            BlogEntry[] listPage = list.Skip((page - 1) * Page.Length).Take(Page.Length).ToArray();

            BlogEntryDto[] listDto = new BlogEntryDto[listPage.Length];
            for (int index = 0; index < listDto.Length; index++) listDto[index] = new BlogEntryDto(listPage[index]);

            return new Page<BlogEntryDto>()
            {
                List = listDto,
                Number = page,
                TotalLength = list.Count()
            };
        }

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

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlogEntry(int id, BlogEntry blogEntry)
        {
            if (id != blogEntry.Id)
            {
                return BadRequest();
            }

            try
            {
                BlogEntry foundEntry = await _context.BlogEntries.AsNoTracking().SingleOrDefaultAsync(entry => entry.Id == id);
                blogEntry.PublicationDate = foundEntry.PublicationDate;

                _context.Entry(blogEntry).State = EntityState.Modified;

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

        [HttpPost]
        public async Task<ActionResult<BlogEntry>> PostBlogEntry(BlogEntry blogEntry)
        {
            blogEntry.PublicationDate = DateTime.Now;
            _context.BlogEntries.Add(blogEntry);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBlogEntry", new { id = blogEntry.Id }, blogEntry);
        }

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
