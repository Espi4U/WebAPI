using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Models.Responses
{
    public class BaseResponse
    {
        [JsonProperty("iserror")]
        public bool IsError { get; set; } = false;
        [JsonProperty("message")]
        public string Message { get; set; } = "";
    }
}
