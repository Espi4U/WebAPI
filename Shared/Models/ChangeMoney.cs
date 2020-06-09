using Newtonsoft.Json;
using System;

namespace WebAPI.Models.APIModels
{
    public class ChangeMoney
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int PurseId { get; set; }
        public Purse Purse { get; set; }
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public int? FamilyId { get; set; }
        public Family Family { get; set; }
        public int? PersonId { get; set; }
        public Person Person { get; set; }
    }
}
