using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Requests.FamiliesRequests;
using Shared.Models.Responses;
using WebAPI.Models;
using WebAPI.Models.APIModels;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("families")]
    public class FamiliesController : ControllerBase
    {
        private readonly FamilyService _familyService;
        public FamiliesController(FamilyService familyService)
        {
            _familyService = familyService;
        }

        [Route("add_family"), HttpPost]
        public BaseResponse AddFamily([FromBody]FamilyRequest request) =>
            _familyService.AddFamily(request);

    }
}
