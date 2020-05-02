using Shared.Models;
using System.Collections.Generic;

namespace WebAPI.Models.APIModels
{
    public class Family
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Person> Persons { get; set; }
        public List<ChangeMoney> ChangeMoneys { get; set; }
        public List<Purpose> Purposes { get; set; }
        public List<Purse> Purses { get; set; }
        public List<Report> Reports { get; set; }
        public List<InviteKey> InviteKeys { get; set; }
    }
}
