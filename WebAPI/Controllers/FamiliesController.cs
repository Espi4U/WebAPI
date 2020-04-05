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
    [Route("api/v1/families")]
    public class FamiliesController : ControllerBase
    {
        private readonly FamilyService _familyService;
        public FamiliesController(FamilyService familyService)
        {
            _familyService = familyService;
        }

        [Route("add_family"), HttpPost]
        public async Task<BaseResponse> AddFamilyAsync([FromBody]FamilyRequest request) =>
            await Task.Run(() => _familyService.AddFamily(request));


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            using FamilyFinanceContext context = new FamilyFinanceContext();
            return Ok("Test success");
        }
    }
}
