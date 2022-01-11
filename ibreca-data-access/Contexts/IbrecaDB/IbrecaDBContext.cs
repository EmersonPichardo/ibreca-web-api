using ibreca_data_access.Contexts.IbrecaDB.Models;
using Microsoft.EntityFrameworkCore;

namespace ibreca_data_access.Contexts.IbrecaDB
{
    public class IbrecaDBContext : DbContext
    {
        public DbSet<BlogEntry> BlogEntries { get; set; }
        public DbSet<Announcement> Announcements { get; set; }

        public IbrecaDBContext(DbContextOptions<IbrecaDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new BlogEntry.Configuration());
            modelBuilder.ApplyConfiguration(new Announcement.Configuration());
        }
    }
}
