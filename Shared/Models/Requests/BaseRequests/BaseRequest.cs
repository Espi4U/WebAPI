using Newtonsoft.Json;

namespace WebAPI.Models.APIModels.Requests
{
    public class BaseRequest
    {
        public int PersonId { get; set; }
        public int FamilyId { get; set; }
    }
}
