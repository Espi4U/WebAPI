using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.APIModels
{
    public class Currency
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }

        public int? ChangeMoneyId { get; set; }
        public ChangeMoney ChangeMoney { get; set; }

        public int? PurposeId { get; set; }
        public Purpose Purpose { get; set; }

        public int? PurseId { get; set; }
        public Purse Purse { get; set; }
    }
}
