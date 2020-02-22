using Newtonsoft.Json;

namespace WebAPI.Models.APIModels
{
    public class Purse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; } // назва гаманця

        [JsonProperty("size")]
        public int Size { get; set; } // розмір гаманця

        [JsonProperty("currency")]
        public Currency Currency { get; set; } // валюта гаманця

        [JsonProperty("familyid")]
        public int? FamilyId { get; set; }

        [JsonProperty("family")]
        public Family Family { get; set; } // сім'я власник гаманця

        [JsonProperty("personid")]
        public int? PersonId { get; set; }

        [JsonProperty("person")]
        public Person Person { get; set; } // член сім'ї власник гаманця
    }
}
