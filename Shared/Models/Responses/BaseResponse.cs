using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Models.Responses
{
    public class BaseResponse : ApiResponse
    {
        [JsonProperty("iserror")]
        public bool BaseIsSuccess { get; set; } = true;
        [JsonProperty("message")]
        public string BaseMessage { get; set; } = "";
    }
}
