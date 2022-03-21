using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ibreca_data_access.Contexts.IbrecaDB.Models
{
    public class Announcement
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string UrlAssetId { get; set; }
        public DateTime? ShowUntil { get; set; }

        public class Configuration : IEntityTypeConfiguration<Announcement>
        {
            public void Configure(EntityTypeBuilder<Announcement> builder)
            {
                builder.ToTable("announcements");
                builder.HasKey(announcement => announcement.Id).HasName("pk_announcements");

                builder.Property(announcement => announcement.Id).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
                builder.Property(announcement => announcement.Title).HasColumnType("varchar(100)").IsRequired();
                builder.Property(announcement => announcement.UrlAssetId).HasColumnType("varchar(100)").IsRequired();
                builder.Property(announcement => announcement.Url).HasColumnType("varchar(512)").IsRequired();
                builder.Property(announcement => announcement.ShowUntil).HasColumnType("datetime");
            }
        }
    }
}
