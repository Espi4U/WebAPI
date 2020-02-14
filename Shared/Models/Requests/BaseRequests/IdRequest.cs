using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.APIModels.Requests
{
    public class IdRequest
    {
        public int? PersonID { get; set; }
        public int? FamilyID { get; set; }
    }
}
