using Newtonsoft.Json;

namespace WebAPI.Models.APIModels
{
    public class Purse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

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
