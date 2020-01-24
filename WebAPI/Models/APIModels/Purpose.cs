namespace WebAPI.Models.APIModels
{
    public class Purpose
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal FinalSize { get; set; }
        public decimal CurrentSize { get; set; }
        public Currency Currency { get; set; }
    }
}
