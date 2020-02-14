﻿using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.APIModels
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }

        public int? ChangeMoneyId { get; set; }
        public ChangeMoney ChangeMoney { get; set; }
    }
}