using FourCharacterPhrase.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FourCharacterPhrase.Server
{
    public class AppDbContext : DbContext
    {
        public DbSet<AnswerNumberEntity> AnswerNumberEntitys { get; set; }
        public DbSet<CellEntity> CellEntitys { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source='data.db'");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnswerNumberEntity>().ToTable("D_AnswerNumber").HasKey(c => new { c.Name });
            modelBuilder.Entity<CellEntity>().ToTable("D_Cell").HasKey(c => new { c.Name, c.No });
        }
    }
}
