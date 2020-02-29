using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Requests.PursesRequests;
using Shared.Models.Responses;
using Shared.Models.Responses.PursesResponses;
using WebAPI.Models.APIModels.Requests;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("purses")]
    public class PursesController : ControllerBase
    {
        private readonly PurseService _purseService;
        public PursesController(PurseService purseService)
        {
            _purseService = purseService;
        }

        [Route("get_purses_by_id"), HttpPost]
        public ListPursesResponse GetPursesById([FromBody]IdRequest request) =>
            _purseService.GetPursesById(request);

        [Route("add_new_purse"), HttpPost]
        public BaseResponse AddNewReport([FromBody]PurseRequest request) =>
            _purseService.AddNewPurse(request);

        [Route("delete_purse_by_id"), HttpPost]
        public BaseResponse DeleteReportById([FromBody]PurseRequest request) =>
            _purseService.DeletePurseById(request);
    }
}
