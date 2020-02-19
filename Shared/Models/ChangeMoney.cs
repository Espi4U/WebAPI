using Newtonsoft.Json;
using System;

namespace WebAPI.Models.APIModels
{
    public class ChangeMoney
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("category")]
        public Category Category { get; set; }

        [JsonProperty("currency")]
        public Currency Currency { get; set; }

        [JsonProperty("familyid")]
        public int? FamilyId { get; set; }

        [JsonProperty("family")]
        public Family Family { get; set; }

        [JsonProperty("personid")]
        public int? PersonId { get; set; }

        [JsonProperty("person")]
        public Person Person { get; set; }
    }
}
