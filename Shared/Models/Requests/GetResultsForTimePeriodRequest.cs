using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Models.APIModels.Requests;

namespace Shared.Models.Requests.BaseRequests
{
    public class GetResultsForTimePeriodRequest : BaseRequest
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime Now { get; set; }
        public string Type { get; set; }
    }
}
