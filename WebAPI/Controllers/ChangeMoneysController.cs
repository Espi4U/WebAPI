using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Requests.BaseRequests;
using Shared.Models.Requests.ChangeMoneyRequests;
using Shared.Models.Responses;
using Shared.Models.Responses.ChangeMoneysResponses;
using WebAPI.Models.APIModels.Requests;
using WebAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/v1/changemoneys")]
    public class ChangeMoneysController : ControllerBase
    {
        private readonly ChangeMoneyService _changeMoneyService;

        public ChangeMoneysController(ChangeMoneyService changeMoneyService)
        {
            _changeMoneyService = changeMoneyService;
        }

        [HttpPost, Route("get_all_by_type")]
        public async Task<ListChangeMoneysResponse> GetIncomesOrExpensesAsync([FromBody]GetIncomesOrExpensesRequest request) =>
            await Task.Run(() => _changeMoneyService.GetIncomesOrExpenses(request));

        [HttpPost, Route("get_all")]
        public async Task<ListChangeMoneysResponse> GetAllIncomesOrExpensesAsync([FromBody]BaseRequest request) =>
            await Task.Run(() => _changeMoneyService.GetAllIncomesOrExpenses(request));

        [HttpPost, Route("get_largest")]
        public async Task<ChangeMoneyResponse> GetLargestIncomeOrExpenseAsync([FromBody]GetIncomesOrExpensesRequest request) =>
            await Task.Run(() => _changeMoneyService.GetLargestIncomeOrExpense(request));

        [HttpPost, Route("get_smallest")]
        public async Task<ChangeMoneyResponse> GetSmallestIncomeOrExpenseAsync([FromBody]GetIncomesOrExpensesRequest request) =>
            await Task.Run(() => _changeMoneyService.GetSmallestIncomeOrExpense(request));

        [HttpPost, Route("get_per_time_period")]
        public async Task<ListChangeMoneysResponse> GetResultForTimePeriodAsync([FromBody]GetResultsForTimePeriodRequest request) =>
            await Task.Run(() => _changeMoneyService.GetResultForTimePeriod(request));

        [HttpPost, Route("add")]
        public async Task<BaseResponse> AddIncomeOrExpenseAsync([FromBody]ChangeMoneyRequest request) =>
            await Task.Run(() => _changeMoneyService.AddIncomeOrExpense(request));

        [HttpPost, Route("data_by_category_to_chart")]
        public async Task<DataByCategotyToChartResponse> GetDataByCategoryToChart([FromBody]BaseRequest request) =>
            await Task.Run(() => _changeMoneyService.GetDataByCategoryToChart(request));
    }
}
