using HtmlAgilityPack;
using ibreca_data_access.Contexts.IbrecaDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(blogEntry.Body);

            List<string> texts = new List<string>();

            foreach (HtmlNode node in htmlDocument.DocumentNode.DescendantsAndSelf())
            {
                if(texts.Sum(text => text.Length) >= 300) break;

                if (node.NodeType == HtmlNodeType.Text)
                {
                    if (node.InnerText.Trim() != "")
                    {
                        texts.Add(node.InnerText.Trim());
                    }
                }
            }

            Id = blogEntry.Id;
            Title = blogEntry.Title;
            Body = string.Join(". ", texts);
            CoverUrl = blogEntry.CoverUrl;
            PublicationDate = blogEntry.PublicationDate;
            Status = blogEntry.Status;
        }
    }
}
