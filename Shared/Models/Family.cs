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
        public string Name { get; set; } // імя сім'ї

        [JsonProperty("persons")]
        public List<Person> Persons { get; set; } // усі члени сім'ї

        [JsonProperty("changesinmoney")]
        public List<ChangeMoney> ChangesInMoney { get; set; } // загальні сімейні доходи/витрати

        [JsonProperty("purposes")]
        public List<Purpose> Purposes { get; set; } // цілі заощадження сім'ї

        [JsonProperty("purses")]
        public List<Purse> Purses { get; set; } // гаманці сім'ї

        [JsonProperty("reports")]
        public List<Report> Reports { get; set; } // звіти сім'ї

        [JsonProperty("categories")]
        public List<Category> Categories { get; set; } // категорії доходів/витрат сім'ї

        [JsonProperty("currencies")]
        public List<Currency> Currencies { get; set; } // валюти сім'ї
    }
}
