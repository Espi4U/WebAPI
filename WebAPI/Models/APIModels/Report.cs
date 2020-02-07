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
    }
}
