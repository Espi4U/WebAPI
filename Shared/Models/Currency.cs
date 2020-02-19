using Newtonsoft.Json;

namespace WebAPI.Models.APIModels
{
    public class Currency
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("changemoneyid")]
        public int? ChangeMoneyId { get; set; }

        [JsonProperty("changemoney")]
        public ChangeMoney ChangeMoney { get; set; }

        [JsonProperty("purposeid")]
        public int? PurposeId { get; set; }

        [JsonProperty("purpose")]
        public Purpose Purpose { get; set; }

        [JsonProperty("purseid")]
        public int? PurseId { get; set; }

        [JsonProperty("purse")]
        public Purse Purse { get; set; }
    }
}
