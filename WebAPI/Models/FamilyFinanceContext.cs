using Microsoft.EntityFrameworkCore;
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
        public DbSet<ChangeMoney> ChangesMoney { get; set; }
        public DbSet<Currency> Currencies { get; set; }

        public FamilyFinanceContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;UserId=root;Password=espi4u;database=espdb1;");
        }
    }
}
