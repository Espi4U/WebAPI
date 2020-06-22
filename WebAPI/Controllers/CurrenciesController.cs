using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Requests.CurrenciesRequests;
using Shared.Models.Responses;
using Shared.Models.Responses.CurrenciesResponses;
using WebAPI.Models;
using WebAPI.Models.APIModels.Requests;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/v1/currencies")]
    public class CurrenciesController : ControllerBase
    {
        private readonly CurrencyService _currencyService;
        public CurrenciesController(CurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [Route("get_currencies"), HttpPost]
        public ListCurrenciesResponse GetCurrencies([FromBody]BaseRequest request) =>
            _currencyService.GetCurrencies(request);
    }
}
