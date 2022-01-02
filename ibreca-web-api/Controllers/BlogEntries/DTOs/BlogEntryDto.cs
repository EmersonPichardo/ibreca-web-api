using ibreca_data_access.Contexts.IbrecaDB.Models;
using System;

namespace ibreca_web_api.Controllers.BlogEntries
{
    public class BlogEntryDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string CoverUrl { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Status { get; set; }

        public BlogEntryDto(BlogEntry blogEntry)
        {
            Id = blogEntry.Id;
            Title = blogEntry.Title;
            Body = blogEntry.Body;
            CoverUrl = blogEntry.CoverUrl;
            PublicationDate = blogEntry.PublicationDate;
            Status = blogEntry.Status;
        }
    }
}
