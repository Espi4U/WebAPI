using Newtonsoft.Json;
using Shared.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.APIModels.Responses.PersonsControllerResponses
{
    public class ListPersonsResponse : BaseResponse
    {
        public List<Person> Persons { get; set; }
    }
}
