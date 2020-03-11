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
    [Route("api/v1/persons")]
    public class PersonsController : ControllerBase
    {
        private readonly PersonService _personService;
        public PersonsController(PersonService personService)
        {
            _personService = personService;
        }

        [Route("get_persons"), HttpPost]
        public async Task<ListPersonsResponse> GetPersonsAsync([FromBody]BaseRequest request) =>
            await Task.Run(()=> _personService.GetPersons(request));

        [Route("add_person"), HttpPost]
        public async Task<BaseResponse> AddPersonAsync([FromBody]PersonRequest request) =>
            await Task.Run(() => _personService.AddPerson(request));
    }
}
