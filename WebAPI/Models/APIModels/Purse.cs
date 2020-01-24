namespace WebAPI.Models.APIModels
{
    public class Purse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Size { get; set; }
        public Currency Currency { get; set; }
    }
}
