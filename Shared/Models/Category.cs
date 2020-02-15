namespace WebAPI.Models.APIModels
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int? ChangeMoneyId { get; set; }
        public ChangeMoney ChangeMoney { get; set; }
    }
}
