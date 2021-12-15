using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ibreca_data_access.Contexts.IbrecaDB.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CoverUrl { get; set; }
        public string HeaderUrl { get; set; }
        public string Body { get; set; }
        public string Status { get; set; }

        public class Configuration : IEntityTypeConfiguration<Post>
        {
            public void Configure(EntityTypeBuilder<Post> builder)
            {
                builder.ToTable("posts");
                builder.HasKey(post => post.Id).HasName("pk_posts");

                builder.Property(post => post.Id).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
                builder.Property(post => post.Title).HasColumnType("varchar(100)").IsRequired();
                builder.Property(post => post.CoverUrl).HasColumnType("varchar(256)");
                builder.Property(post => post.HeaderUrl).HasColumnType("varchar(256)");
                builder.Property(post => post.Body).HasColumnType("text").IsRequired();
                builder.Property(post => post.Status).HasColumnType("varchar(20)").IsRequired();
            }
        }
    }
}
