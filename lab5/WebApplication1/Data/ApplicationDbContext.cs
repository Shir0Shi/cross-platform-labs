using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebApplication1.Entities;

namespace WebApplication1.Data
{
    public class DbContext : IdentityDbContext<L5User>
    {
        public DbContext(DbContextOptions<DbContext> options)
            : base(options)
        { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<L5User>(entity =>
            {
                entity.ToTable("L5User"); // Specifies the table name for the entity
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });
            //modelBuilder.Entity<L5User>(user => user.ToTable(name: "L5User"));
            //modelBuilder.ApplyConfiguration(new UserConfig());
        }
    }
}
