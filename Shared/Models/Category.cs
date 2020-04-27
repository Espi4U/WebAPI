using System.Collections.Generic;

namespace WebAPI.Models.APIModels
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ChangeMoney> ChangeMoneys { get; set; }
    }
}