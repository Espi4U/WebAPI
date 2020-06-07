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
        public DbSet<InviteKey> InviteKeys { get; set; }

        public FamilyFinanceContext()
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseMySql("server=localhost;UserId=root;Password=espi4u;database=espdb1;");
            optionsBuilder.UseMySql("server=db4free.net;UserId=dyplomwebapi7355;Password=dyplomwebapi7355;database=dyplomwebapi7355;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
