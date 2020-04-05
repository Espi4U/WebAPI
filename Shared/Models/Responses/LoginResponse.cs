using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Models.Responses
{
    public class LoginResponse : BaseResponse
    {
        [JsonProperty("personid")]
        public int PersonId { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
