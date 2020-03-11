using Newtonsoft.Json;

namespace WebAPI.Models.APIModels.Requests
{
    public class BaseRequest
    {
        [JsonProperty("personid")]
        public int? PersonId { get; set; }
        [JsonProperty("familyid")]
        public int? FamilyId { get; set; }

        [JsonProperty("personaccesstoken")]
        public string PersonAccessToken { get; set; }

        [JsonProperty("familyaccesstoken")]
        public string FamilyAccessToken { get; set; }
    }
}
