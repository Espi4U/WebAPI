using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.APIModels
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsHead { get; set; }
        [Required]
        [MaxLength(4)]
        public int PersonalPINCode { get; set; }
        public List<ChangeMoney> ChangesInMoney { get; set; }
        public List<Purpose> Purposes { get; set; }
        public List<Purse> Purses { get; set; }
        public List<Report> Reports { get; set; }
    }
}
