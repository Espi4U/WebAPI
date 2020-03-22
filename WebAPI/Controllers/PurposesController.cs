using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Requests.PurposesRequests;
using Shared.Models.Responses;
using Shared.Models.Responses.PurposesResponses;
using WebAPI.Models;
using WebAPI.Models.APIModels.Requests;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/purposes")]
    public class PurposesController : ControllerBase
    {
        private readonly PurposeService _purposeService;

        public PurposesController(PurposeService purposeService)
        {
            _purposeService = purposeService;
        }

        [Route("get_purposes"), HttpPost]
        public ListPurposesResponse GetPurses([FromBody]BaseRequest request) =>
            _purposeService.GetPurposes(request);

        [Route("add_purpose"), HttpPost]
        public BaseResponse AddPurse([FromBody]PurposeRequest request) =>
            _purposeService.AddPurpose(request);

        [Route("delete_purpose"), HttpPost]
        public BaseResponse DeletePurse([FromBody]PurposeRequest request) =>
            _purposeService.DeletePurpose(request);

        [Route("get"), HttpGet]
        public string Get()
        {
            using (var context = new FamilyFinanceContext())
            {
                return "AAA";
            }
        }
    }
}
