using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.APIModels
{
    public class ChangeMoney
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Size { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public Category Category { get; set; }
        [Required]
        public Currency Currency { get; set; }
    }
}
