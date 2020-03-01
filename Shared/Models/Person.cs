using Newtonsoft.Json;
using System.Collections.Generic;
namespace WebAPI.Models.APIModels
{
    public class Person
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; } // ім'я члена сім'ї

        [JsonProperty("ishead")]
        public bool IsHead { get; set; } // чи даний член сім'ї єголової сім'ї

        [JsonProperty("changesinmoney")]
        public List<ChangeMoney> ChangesInMoney { get; set; } // витрати/доходи члена сім'ї

        [JsonProperty("purposes")]
        public List<Purpose> Purposes { get; set; } // цілі заощадження члена сім'ї

        [JsonProperty("purses")]
        public List<Purse> Purses { get; set; } // гаманці члена сім'ї

        [JsonProperty("reports")]
        public List<Report> Reports { get; set; } // звіти члена сім'ї

        [JsonProperty("familyid")]
        public int FamilyId { get; set; }

        [JsonProperty("family")]
        public Family Family { get; set; } // до якої сім'ї відноситься даний член сім'ї

        [JsonProperty("categories")]
        public List<Category> Categories { get; set; } // категорії доходів/витрат члена сім'ї

        [JsonProperty("currencies")]
        public List<Currency> Currencies { get; set; } // валюти члена сім'ї


        // дані для авторизації/реєстрації
        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("passwordhash")]
        public string PasswordHash { get; set; }

        [JsonProperty("pincode")]
        public int PINCode { get; set; }
    }
}
