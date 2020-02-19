using Newtonsoft.Json;
using System;

namespace WebAPI.Models.APIModels
{
    public class Report
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

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
