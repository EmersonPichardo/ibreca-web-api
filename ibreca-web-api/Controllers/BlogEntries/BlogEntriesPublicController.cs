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
    [Route("api/[controller]")]
    [ApiController]
    public class BlogEntriesPublicController : ControllerBase
    {
        private readonly IbrecaDBContext _context;

        public BlogEntriesPublicController(IbrecaDBContext context) => _context = context;

        [HttpGet("page/{page}/{search}/{from}/{to}")]
        public async Task<ActionResult<Page<BlogEntryDto>>> GetBlogEntriesPage(int page, string search = null, DateTime? from = null, DateTime? to = null)
        {
            List<BlogEntry> list =
                await _context.BlogEntries
                    .Where(entry =>
                        (string.IsNullOrWhiteSpace(search) ? true : entry.Title.Contains(search)) &&
                        (from.HasValue ? (entry.PublicationDate >= from.Value) : true) &&
                        (to.HasValue ? (entry.PublicationDate <= to.Value) : true)
                    )
                    .OrderBy(entry => entry.PublicationDate)
                    .ToListAsync();

            BlogEntry[] listPage = list.Skip((page - 1) * Page.Length).Take(Page.Length).ToArray();

            BlogEntryDto[] listDto = new BlogEntryDto[listPage.Length];
            for (int index = 0; index < listDto.Length; index++) listDto[index] = new BlogEntryDto(listPage[index]);

            return await Task.FromResult(
                new Page<BlogEntryDto>()
                {
                    List = listDto,
                    Number = page,
                    TotalLength = list.Count()
                }
            );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BlogEntry>> GetBlogEntry(int id)
        {
            BlogEntry blogEntry = await _context.BlogEntries.FindAsync(id);

            if (blogEntry == null) await Task.FromResult(NotFound());

            return await Task.FromResult(blogEntry);
        }
    }
}
