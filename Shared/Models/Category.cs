using Newtonsoft.Json;

namespace WebAPI.Models.APIModels
{
    public class Category
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; } // назва категорії

        [JsonProperty("changemoneyid")]
        public int? ChangeMoneyId { get; set; }

        [JsonProperty("changemoney")]
        public ChangeMoney ChangeMoney { get; set; } // витрата/дохід в даній категорії

        [JsonProperty("familyid")]
        public int? FamilyId { get; set; }

        [JsonProperty("family")]
        public Family Family { get; set; } // сім'я власник категорії

        [JsonProperty("personid")]
        public int? PersonId { get; set; }

        [JsonProperty("person")]
        public Person Person { get; set; } // член сім'ї власник категорії
    }
}
