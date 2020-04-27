using Newtonsoft.Json;
using System;

namespace WebAPI.Models.APIModels
{
    public class Report
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int FamilyId { get; set; }
        public Family Family { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}