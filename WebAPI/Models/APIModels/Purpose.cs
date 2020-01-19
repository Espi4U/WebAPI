using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.APIModels
{
    public class Purpose
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal FinalSize { get; set; }
        public decimal CurrentSize { get; set; }
    }
}
