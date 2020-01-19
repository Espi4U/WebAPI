using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.APIModels
{
    public class ChangeMoney
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Size { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
    }
}
