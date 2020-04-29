using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Models;
using WebAPI.Models.APIModels;

namespace WebAPI.Models
{
    public class FamilyFinanceContext : DbContext
    {
        public DbSet<Family> Families { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Purse> Purses { get; set; }
        public DbSet<Purpose> Purposes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ChangeMoney> ChangeMoneys { get; set; }
        public DbSet<Currency> Currencies { get; set; }

        public FamilyFinanceContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;UserId=root;Password=espi4u;database=espdb1;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().Property(x => x.Id).ValueGeneratedNever();
            modelBuilder.Entity<Currency>().Property(x => x.Id).ValueGeneratedNever();
        }
    }
}
