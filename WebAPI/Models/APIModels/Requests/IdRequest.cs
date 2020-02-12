using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.APIModels.Requests
{
    public class IdRequest
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
