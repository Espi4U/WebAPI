using Newtonsoft.Json;
using System.Collections.Generic;
namespace WebAPI.Models.APIModels
{
    public class Person
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("ishead")]
        public bool IsHead { get; set; }

        [JsonProperty("personalpincode")]
        public int PersonalPINCode { get; set; }

        [JsonProperty("changesinmoney")]
        public List<ChangeMoney> ChangesInMoney { get; set; }

        [JsonProperty("purposes")]
        public List<Purpose> Purposes { get; set; }

        [JsonProperty("purses")]
        public List<Purse> Purses { get; set; }

        [JsonProperty("reports")]
        public List<Report> Reports { get; set; }

        [JsonProperty("familyid")]
        public int FamilyId { get; set; }

        [JsonProperty("family")]
        public Family Family { get; set; }
    }
}
