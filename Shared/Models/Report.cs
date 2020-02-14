using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.APIModels
{
    public class Report
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Text { get; set; }

        public int? FamilyId { get; set; }
        public Family Family { get; set; }

        public int? PersonId { get; set; }
        public Person Person { get; set; }
    }
}
