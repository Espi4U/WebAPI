namespace WebAPI.Models.APIModels
{
    public class Currency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int? ChangeMoneyId { get; set; }
        public ChangeMoney ChangeMoney { get; set; }
        public int? PurposeId { get; set; }
        public Purpose Purpose { get; set; }
        public int? PurseId { get; set; }
        public Purse Purse { get; set; }
    }
}
