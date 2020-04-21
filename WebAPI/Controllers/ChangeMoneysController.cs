using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Requests.BaseRequests;
using Shared.Models.Requests.ChangeMoneyRequests;
using Shared.Models.Responses;
using Shared.Models.Responses.ChangeMoneysResponses;
using WebAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/changemoneys")]
    public class ChangeMoneysController : ControllerBase
    {
        private readonly ChangeMoneyService _changeMoneyService;

        public ChangeMoneysController(ChangeMoneyService changeMoneyService)
        {
            _changeMoneyService = changeMoneyService;
        }

        [HttpPost, Route("get_all")]
        public ListChangeMoneysResponse GetIncomesOrExpenses([FromBody]GetIncomesOrExpensesRequest request) =>
            _changeMoneyService.GetIncomesOrExpenses(request);

        [HttpPost, Route("get_largest")]
        public ChangeMoneyResponse GetLargestIncomeOrExpense([FromBody]GetIncomesOrExpensesRequest request) =>
            _changeMoneyService.GetLargestIncomeOrExpense(request);

        [HttpPost, Route("get_smallest")]
        public ChangeMoneyResponse GetSmallestIncomeOrExpense([FromBody]GetIncomesOrExpensesRequest request) =>
            _changeMoneyService.GetSmallestIncomeOrExpense(request);

        [HttpPost, Route("get_per_time_period")]
        public ListChangeMoneysResponse GetResultForTimePeriod([FromBody]GetResultsForTimePeriodRequest request) =>
            _changeMoneyService.GetResultForTimePeriod(request);

        [HttpPost, Route("add")]
        public BaseResponse AddIncomeOrExpense([FromBody]ChangeMoneyRequest request) =>
            _changeMoneyService.AddIncomeOrExpense(request);
    }
}
