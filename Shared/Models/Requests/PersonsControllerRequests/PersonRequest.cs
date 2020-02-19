using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.APIModels.Requests.PersonsControllerRequests
{
    public class PersonRequest
    {
        [JsonProperty("person")]
        public Person Person { get; set; }
    }
}
