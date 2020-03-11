using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    [ApiController]
    [Route("api/v1/currencies")]
    public class CurrenciesController : ControllerBase
    {
        private readonly CurrencyService _currencyService;
        public CurrenciesController(CurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [Route("add_new_currency"), HttpPost]
        public BaseResponse AddNewCurrency([FromBody]CurrencyRequest request) =>
            _currencyService.AddNewCurrency(request);

        [Route("get_all_currencies_by_id"), HttpPost]
        public ListCurrenciesResponse GetAllCurrenciesById([FromBody]BaseRequest request) =>
            _currencyService.GetCurrenciesById(request);

        [Route("delete_currency_by_id"), HttpPost]
        public BaseResponse DeleteCurrencyById([FromBody]CurrencyRequest request) =>
            _currencyService.DeleteCurrencyById(request);
    }
}
