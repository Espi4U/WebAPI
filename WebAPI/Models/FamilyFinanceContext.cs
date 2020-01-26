using Microsoft.EntityFrameworkCore;
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

        public FamilyFinanceContext(DbContextOptions<FamilyFinanceContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
