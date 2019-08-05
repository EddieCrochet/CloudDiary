using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Text;
using CloudDiary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CloudDiary.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<DiaryEntry> DiaryEntries { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = CloudDiary; Trusted_Connection = True; MultipleActiveResultSets = true");
            //configuring the context to the database
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //using fluent API to map database table
            modelBuilder.Entity<DiaryEntry>()
                .ToTable("DiaryEntries");
        }
    }
}
