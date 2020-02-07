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
    }
}
