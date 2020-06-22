using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Requests;
using Shared.Models.Requests.BaseRequests;
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

        [HttpPost, Route("login")]
        public async Task<LoginResponse> Login([FromBody]LoginRequest request) =>
            await Task.Run(() => _personService.Login(request));

        [HttpPost, Route("registration_new")]
        public async Task<BaseResponse> RegistrationNew([FromBody]RegistrationRequest request) =>
            await Task.Run(() => _personService.RegistrationNew(request));

        [HttpPost, Route("registration_new_with_key")]
        public async Task<BaseResponse> RegistrationNewWithKey([FromBody]RegistrationRequest request) =>
            await Task.Run(() => _personService.RegistrationNewWithKey(request));
    }
}
