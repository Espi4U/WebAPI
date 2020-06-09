using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Requests;
using Shared.Models.Requests.PursesRequests;
using Shared.Models.Responses;
using Shared.Models.Responses.PursesResponses;
using WebAPI.Models.APIModels.Requests;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/purses")]
    public class PursesController : ControllerBase
    {
        private readonly PurseService _purseService;
        public PursesController(PurseService purseService)
        {
            _purseService = purseService;
        }

        [Route("get_purses"), HttpPost]
        public ListPursesResponse GetPurses([FromBody]BaseRequest request) =>
            _purseService.GetPurses(request);

        [Route("get_purses_by_currency"), HttpPost]
        public ListPursesResponse GetPursesByCurrency([FromBody]GetPursesByCurrencyRequest request) =>
            _purseService.GetPursesByCurrency(request);

        [Route("add_purse"), HttpPost]
        public BaseResponse AddPurse([FromBody]PurseRequest request) =>
            _purseService.AddPurse(request);

        [Route("delete_purse"), HttpPost]
        public BaseResponse DeletePurse([FromBody]PurseRequest request) =>
            _purseService.DeletePurse(request);
    }
}
