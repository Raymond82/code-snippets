using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MVC_Test.Models.Core
{
    public class ChinookContext : DbContext
    {
        public ChinookContext(DbContextOptions options) : base(options)
        {
        }

        // ok, this is ugly but it should at least find the correct table name for now.
        // TODO: research to get this displayed pluralized but use the singularized tablename.
        // aka. public virtual DbSet<Album> Albums but us Album table.
        public virtual DbSet<Album> Albums { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>().ToTable("Album");
        }
    }
}
