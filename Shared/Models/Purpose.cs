using Newtonsoft.Json;

namespace WebAPI.Models.APIModels
{
    public class Purpose
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FinalSize { get; set; }
        public int CurrentSize { get; set; }
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public int? FamilyId { get; set; }
        public Family Family { get; set; }
        public int? PersonId { get; set; }
        public Person Person { get; set; }

        [JsonIgnore]
        public double PurposeProgress
        {
            get => CurrentSize * 1.0 / FinalSize * 1.0;
        }

        [JsonIgnore]
        public string DescriptionText
        {
            get => FinalSize == CurrentSize ? "Завершено" : $"Необхідно {FinalSize - CurrentSize} з {FinalSize}";
        }

        [JsonIgnore]
        public bool IsCompleted
        {
            get => FinalSize == CurrentSize;
        }
    }
}
