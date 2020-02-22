using Newtonsoft.Json;
using System;

namespace WebAPI.Models.APIModels
{
    public class ChangeMoney
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; } // назва витрати/доходу

        [JsonProperty("size")]
        public int Size { get; set; } // розмір витрати/доходу

        [JsonProperty("date")]
        public DateTime Date { get; set; } // дата витрати/доходу

        [JsonProperty("type")]
        public string Type { get; set; } // дата витрати/доходу (витрати або дохід)

        [JsonProperty("category")]
        public Category Category { get; set; } // категорія витрати/доходу

        [JsonProperty("currency")]
        public Currency Currency { get; set; } // валюта витрати/доходу

        [JsonProperty("familyid")]
        public int? FamilyId { get; set; }

        [JsonProperty("family")]
        public Family Family { get; set; } // родина власник витрати/доходу

        [JsonProperty("personid")]
        public int? PersonId { get; set; }

        [JsonProperty("person")]
        public Person Person { get; set; } // член сім'ї власник витрати/доходу
    }
}
