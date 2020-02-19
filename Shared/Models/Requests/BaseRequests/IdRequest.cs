using Newtonsoft.Json;

namespace WebAPI.Models.APIModels.Requests
{
    public class IdRequest
    {
        [JsonProperty("personid")]
        public int? PersonID { get; set; }
        [JsonProperty("familyid")]
        public int? FamilyID { get; set; }
    }
}
