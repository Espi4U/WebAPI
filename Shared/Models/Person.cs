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

        [JsonProperty("role")]
        public string Role { get; set; }

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

        [JsonProperty("categories")]
        public List<Category> Categories { get; set; }

        [JsonProperty("currencies")]
        public List<Currency> Currencies { get; set; }


        // дані для авторизації/реєстрації
        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("passwordhash")]
        public string PasswordHash { get; set; }

        [JsonProperty("pincode")]
        public int PINCode { get; set; }
    }
}
