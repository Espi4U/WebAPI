using Newtonsoft.Json;
using Shared.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.APIModels.Responses
{
    public class ListReporstResponse : BaseResponse
    {
        [JsonProperty("reports")]
        public List<Report> Reports { get; set; }
    }
}
