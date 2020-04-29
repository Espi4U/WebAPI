using Newtonsoft.Json;

namespace WebAPI.Models.APIModels
{
    public class Purpose
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double FinalSize { get; set; }
        public double CurrentSize { get; set; }
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public int? FamilyId { get; set; }
        public Family Family { get; set; }
        public int? PersonId { get; set; }
        public Person Person { get; set; }

        [JsonIgnore]
        public double PurposeProgress
        {
            get => CurrentSize / FinalSize;
        }
    }
}
