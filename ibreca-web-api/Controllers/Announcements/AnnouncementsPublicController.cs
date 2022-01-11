using ibreca_data_access.Contexts.IbrecaDB;
using ibreca_data_access.Contexts.IbrecaDB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ibreca_web_api.Controllers.Announcements
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementsPublicController : ControllerBase
    {
        private readonly IbrecaDBContext _context;

        public AnnouncementsPublicController(IbrecaDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Announcement>>> GetAnnouncements()
        {
            return await Task.FromResult(
                await _context.Announcements
                    .Where(
                        announcement =>
                            !announcement.ShowUntil.HasValue ||
                            announcement.ShowUntil.Value >= DateTime.Now
                    )
                    .ToListAsync()
            );
        }
    }
}
