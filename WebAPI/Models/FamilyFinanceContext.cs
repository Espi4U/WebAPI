using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.APIModels;

namespace WebAPI.Models
{
    public class FamilyFinanceContext
    {
        public IAsyncEnumerable<Family> Families { get; set; }
        public IAsyncEnumerable<Person> Persons { get; set; }
        public IAsyncEnumerable<Report> Reports { get; set; }
        public IAsyncEnumerable<Purse> Purses { get; set; }
        public IAsyncEnumerable<Purpose> Purposes { get; set; }
        public IAsyncEnumerable<Category> Categories { get; set; }
        public IAsyncEnumerable<ChangeMoney> ChangesMoney { get; set; }
        public IAsyncEnumerable<Currency> Currencies { get; set; }
    }
}
