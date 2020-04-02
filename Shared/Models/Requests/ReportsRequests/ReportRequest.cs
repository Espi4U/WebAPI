using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.APIModels.Requests.ReportsControllerRequests
{
    public class ReportRequest
    {
        [JsonProperty("report")]
        public Report Report { get; set; }
    }
}
