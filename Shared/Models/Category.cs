using Newtonsoft.Json;

namespace WebAPI.Models.APIModels
{
    public class Category
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
    }
}
