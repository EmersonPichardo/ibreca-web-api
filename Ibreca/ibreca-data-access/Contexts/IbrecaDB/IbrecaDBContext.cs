﻿using ibreca_data_access.Contexts.IbrecaDB.Models;
using Microsoft.EntityFrameworkCore;

namespace ibreca_data_access.Contexts.IbrecaDB
{
    public class IbrecaDBContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }

        public IbrecaDBContext(DbContextOptions<IbrecaDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new Post.Configuration());
        }
    }
}
