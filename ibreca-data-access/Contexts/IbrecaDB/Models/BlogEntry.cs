using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ibreca_data_access.Contexts.IbrecaDB.Models
{
    public class BlogEntry
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CoverUrl { get; set; }
        public string CoverUrlAssetId { get; set; }
        public string HeaderUrl { get; set; }
        public string Body { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Status { get; set; }

        public class Configuration : IEntityTypeConfiguration<BlogEntry>
        {
            public void Configure(EntityTypeBuilder<BlogEntry> builder)
            {
                builder.ToTable("blogentries");
                builder.HasKey(post => post.Id).HasName("pk_blogentries");

                builder.Property(post => post.Id).HasColumnType("int").UseIdentityColumn().IsRequired();
                builder.Property(post => post.Title).HasColumnType("varchar(100)").IsRequired();
                builder.Property(post => post.CoverUrlAssetId).HasColumnType("varchar(100)");
                builder.Property(post => post.CoverUrl).HasColumnType("varchar(512)");
                builder.Property(post => post.HeaderUrl).HasColumnType("varchar(512)");
                builder.Property(post => post.Body).HasColumnType("text").IsRequired();
                builder.Property(post => post.PublicationDate).HasColumnType("datetime").IsRequired();
                builder.Property(post => post.Status).HasColumnType("varchar(20)").IsRequired();
            }
        }
    }
}
