using ibreca_data_access.Contexts.IbrecaDB;
using ibreca_data_access.Contexts.IbrecaDB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ibreca_web_api.Controllers.Announcements
{
    public class AnnouncementsController : AuthControllerBase
    {
        private readonly IbrecaDBContext _context;

        public AnnouncementsController(IbrecaDBContext context)
        {
            _context = context;
        }

        [HttpGet("page/{page}/{search}")]
        public async Task<ActionResult<Page<Announcement>>> GetAnnouncementsPage(int page, string search = null)
        {
            List<Announcement> list =
                await _context.Announcements
                    .Where(entry => string.IsNullOrWhiteSpace(search) ? true : entry.Title.Contains(search))
                    .ToListAsync();

            Announcement[] listPage = list.Skip((page - 1) * Page.Length).Take(Page.Length).ToArray();

            return new Page<Announcement>()
            {
                List = listPage,
                Number = page,
                TotalLength = list.Count()
            };
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Announcement>> GetAnnouncement(int id)
        {
            var announcement = await _context.Announcements.FindAsync(id);

            if (announcement == null)
            {
                return NotFound();
            }

            return announcement;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnnouncement(int id, Announcement announcement)
        {
            if (id != announcement.Id)
            {
                return BadRequest();
            }

            _context.Entry(announcement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnnouncementExists(id))
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
        public async Task<ActionResult<Announcement>> PostAnnouncement(Announcement announcement)
        {
            _context.Announcements.Add(announcement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnnouncement", new { id = announcement.Id }, announcement);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncement(int id)
        {
            var announcement = await _context.Announcements.FindAsync(id);
            if (announcement == null)
            {
                return NotFound();
            }

            _context.Announcements.Remove(announcement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnnouncementExists(int id)
        {
            return _context.Announcements.Any(e => e.Id == id);
        }
    }
}
