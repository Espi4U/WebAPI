using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Models.APIModels;

namespace Shared.Models.Requests.PursesRequests
{
    public class PurseRequest
    {
        [JsonProperty("purse")]
        public Purse Purse { get; set; }
    }
}
