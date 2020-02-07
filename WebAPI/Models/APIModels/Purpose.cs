using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.APIModels
{
    public class Purpose
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal FinalSize { get; set; }
        [Required]
        public decimal CurrentSize { get; set; }
        [Required]
        public Currency Currency { get; set; }
    }
}
