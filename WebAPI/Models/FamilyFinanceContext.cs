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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Family>().Property(x => x.Name).IsRequired();

            modelBuilder.Entity<Person>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<Person>().Property(x => x.IsHead).IsRequired();
            modelBuilder.Entity<Person>().Property(x => x.FamilyId).IsRequired();
            modelBuilder.Entity<Person>().Property(x => x.Family).IsRequired();

            modelBuilder.Entity<ChangeMoney>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<ChangeMoney>().Property(x => x.Size).IsRequired();
            modelBuilder.Entity<ChangeMoney>().Property(x => x.Date).IsRequired();
            modelBuilder.Entity<ChangeMoney>().Property(x => x.Type).IsRequired();
            modelBuilder.Entity<ChangeMoney>().Property(x => x.Category).IsRequired();
            modelBuilder.Entity<ChangeMoney>().Property(x => x.Currency).IsRequired();

            modelBuilder.Entity<Category>().Property(x => x.Name).IsRequired();

            modelBuilder.Entity<Currency>().Property(x => x.Name).IsRequired();

            modelBuilder.Entity<Purpose>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<Purpose>().Property(x => x.FinalSize).IsRequired();
            modelBuilder.Entity<Purpose>().Property(x => x.CurrentSize).IsRequired();
            modelBuilder.Entity<Purpose>().Property(x => x.Currency).IsRequired();

            modelBuilder.Entity<Purse>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<Purse>().Property(x => x.Size).IsRequired();
            modelBuilder.Entity<Purse>().Property(x => x.Currency).IsRequired();

            modelBuilder.Entity<Report>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<Report>().Property(x => x.Text).IsRequired();
            modelBuilder.Entity<Report>().Property(x => x.Date).IsRequired();
        }
    }
}
