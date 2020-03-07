using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Models.APIModels.Requests;

namespace Shared.Models.Requests.BaseRequests
{
    public class GetResultsForTimePeriodRequest : IdRequest
    {
        [JsonProperty("start")]
        public DateTime Start { get; set; }

        [JsonProperty("end")]
        public DateTime End { get; set; }
    }
}
