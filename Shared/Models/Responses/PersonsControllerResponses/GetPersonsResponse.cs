using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.APIModels.Responses.PersonsControllerResponses
{
    public class GetPersonsResponse
    {
        public List<Person> Persons { get; set; }
    }
}
