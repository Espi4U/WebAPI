using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Models.Responses
{
    public class BaseResponse : ApiResponse
    {
        public bool BaseIsSuccess { get; set; } = true;
        public string BaseMessage { get; set; } = "";
    }
}
