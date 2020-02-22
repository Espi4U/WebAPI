using Newtonsoft.Json;

namespace WebAPI.Models.APIModels
{
    public class Currency
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; } // назва валюти

        [JsonProperty("changemoneyid")]
        public int? ChangeMoneyId { get; set; }

        [JsonProperty("changemoney")]
        public ChangeMoney ChangeMoney { get; set; } // витрата/дохід в даній валюті

        [JsonProperty("purposeid")]
        public int? PurposeId { get; set; }

        [JsonProperty("purpose")]
        public Purpose Purpose { get; set; } // ціль заощадження в даній валюті

        [JsonProperty("purseid")]
        public int? PurseId { get; set; }

        [JsonProperty("purse")]
        public Purse Purse { get; set; } // гаманець в даній валюті

        [JsonProperty("personid")]
        public int? PersonId { get; set; }

        [JsonProperty("person")]
        public Person Person { get; set; } // член сім'ї власник валюти

        [JsonProperty("familyid")]
        public int? FamilyId { get; set; }

        [JsonProperty("family")]
        public Family Family { get; set; } // сім'я власник валюти
    }
}
