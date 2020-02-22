using Newtonsoft.Json;

namespace WebAPI.Models.APIModels
{
    public class Purpose
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; } // назва цілі заощадження

        [JsonProperty("finalsize")]
        public int FinalSize { get; set; } // кінцева ціль заощадження

        [JsonProperty("currentsize")]
        public int CurrentSize { get; set; } // теперішня ціль заощадження

        [JsonProperty("currency")]
        public Currency Currency { get; set; } // валюта цілі заощадження

        [JsonProperty("familyid")]
        public int? FamilyId { get; set; }

        [JsonProperty("family")]
        public Family Family { get; set; } // сім'я власник цілі заощадження

        [JsonProperty("personid")]
        public int? PersonId { get; set; }

        [JsonProperty("person")]
        public Person Person { get; set; }  // член сім'ї власник цілі заощадження
    }
}
