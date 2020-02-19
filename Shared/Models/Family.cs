using Newtonsoft.Json;
using Shared.Models;
using System.Collections.Generic;

namespace WebAPI.Models.APIModels
{
    public class Family
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("persons")]
        public List<Person> Persons { get; set; }

        [JsonProperty("changesinmoney")]
        public List<ChangeMoney> ChangesInMoney { get; set; }

        [JsonProperty("purposes")]
        public List<Purpose> Purposes { get; set; }

        [JsonProperty("purses")]
        public List<Purse> Purses { get; set; }

        [JsonProperty("reports")]
        public List<Report> Reports { get; set; }
    }
}
