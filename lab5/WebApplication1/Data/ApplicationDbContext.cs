using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebApplication1.Entities;

namespace WebApplication1.Data
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbContext(DbContextOptions<DbContext> options): base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<L5User>(appUser => appUser.ToTable(name: "L5User"));
        }
    }
}
