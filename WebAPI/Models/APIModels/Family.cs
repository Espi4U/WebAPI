using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.APIModels
{
    public class Family
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Person> Persons { get; set; }
        public List<ChangeMoney> ChangesInMoney { get; set; }
        public List<Purpose> Purposes { get; set; }
        public List<Purse> Purses { get; set; }
        public List<Report> Reports { get; set; }
    }
}
