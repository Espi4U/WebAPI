using Newtonsoft.Json;

namespace WebAPI.Models.APIModels
{
    public class Purse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public int? FamilyId { get; set; }
        public Family Family { get; set; }
        public int? PersonId { get; set; }
        public Person Person { get; set; }
    }
}
