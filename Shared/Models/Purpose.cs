using Newtonsoft.Json;

namespace WebAPI.Models.APIModels
{
    public class Purpose
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("finalsize")]
        public int FinalSize { get; set; }

        [JsonProperty("currentsize")]
        public int CurrentSize { get; set; }

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
