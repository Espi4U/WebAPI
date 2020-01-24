using System;

namespace WebAPI.Models.APIModels
{
    public class ChangeMoney
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Size { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public Category Category { get; set; }
        public Currency Currency { get; set; }
    }
}
