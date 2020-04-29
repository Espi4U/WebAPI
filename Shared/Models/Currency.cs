using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models.APIModels
{
    public class Currency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ChangeMoney> ChangeMoneys { get; set; }
        public List<Purpose> Purposes { get; set; }
        public List<Purse> Purses { get; set; }
    }
}
