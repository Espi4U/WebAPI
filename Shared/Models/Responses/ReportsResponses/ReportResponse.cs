using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Models.APIModels;

namespace Shared.Models.Responses.ReportsResponses
{
    public class ReportResponse : BaseResponse
    {
        [JsonProperty("report")]
        public Report Report { get; set; }
    }
}
