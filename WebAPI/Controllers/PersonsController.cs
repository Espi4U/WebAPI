using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Responses;
using WebAPI.Models;
using WebAPI.Models.APIModels;
using WebAPI.Models.APIModels.Requests;
using WebAPI.Models.APIModels.Requests.PersonsControllerRequests;
using WebAPI.Models.APIModels.Responses.PersonsControllerResponses;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("persons")]
    public class PersonsController : ControllerBase
    {
        private readonly PersonService _personService;
        public PersonsController(PersonService personService)
        {
            _personService = personService;
        }

        [Route("get_persons"), HttpPost]
        public ListPersonsResponse GetPersons([FromBody]IdRequest request) =>
            _personService.GetPersons(request);

        [Route("add_person"), HttpPost]
        public BaseResponse AddPerson([FromBody]PersonRequest request) =>
            _personService.AddPerson(request);
    }
}
