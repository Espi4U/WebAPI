using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Models.APIModels;

namespace Shared.Models.Requests.FamiliesRequests
{
    public class FamilyRequest
    {
        [JsonProperty("family")]
        public Family Family { get; set; }
    }
}
