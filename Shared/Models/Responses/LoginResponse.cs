using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Models.Responses
{
    public class LoginResponse : BaseResponse
    {
        [JsonProperty("familyid")]
        public int FamilyId { get; set; }

        [JsonProperty("personid")]
        public int PersonId { get; set; }

        [JsonProperty("familyaccesstoken")]
        public string FamilyAccessToken { get; set; }

        [JsonProperty("personaccesstoken")]
        public string PersonAccessToken { get; set; }
    }
}
