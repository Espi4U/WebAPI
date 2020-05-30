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
        public ListPurposesResponse GetPurposes([FromBody]BaseRequest request) =>
            _purposeService.GetPurposes(request);

        [Route("add_purpose"), HttpPost]
        public BaseResponse AddPurpose([FromBody]PurposeRequest request) =>
            _purposeService.AddPurpose(request);

        [Route("delete_purpose"), HttpPost]
        public BaseResponse DeletePurpose([FromBody]PurposeRequest request) =>
            _purposeService.DeletePurpose(request);

        [Route("update_purpose"), HttpPost]
        public BaseResponse UpdatePurpose([FromBody]PurposeRequest request) =>
            _purposeService.UpdatePurpose(request);
    }
}
