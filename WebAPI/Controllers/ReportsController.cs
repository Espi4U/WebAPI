using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shared.Models.Requests;
using Shared.Models.Requests.BaseRequests;
using Shared.Models.Requests.ChangeMoneyRequests;
using Shared.Models.Responses;
using Shared.Models.Responses.ReportsResponses;
using WebAPI.Models;
using WebAPI.Models.APIModels;
using WebAPI.Models.APIModels.Requests;
using WebAPI.Models.APIModels.Requests.ReportsControllerRequests;
using WebAPI.Models.APIModels.Responses;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/v1/reports")]
    public class ReportsController : ControllerBase
    {
        private readonly ReportService _reportService;
        private readonly ChangeMoneyService _changeMoneyService;
        public ReportsController(
            ReportService reportService,
            ChangeMoneyService changeMoneyService)
        {
            _reportService = reportService;
            _changeMoneyService = changeMoneyService;
        }

        [Route("get_reports"), HttpPost]
        public ListReportsResponse GetAllReportsById([FromBody]BaseRequest request) =>
            _reportService.GetReports(request);


        [Route("delete_report"), HttpPost]
        public BaseResponse DeleteReportById([FromBody]DeleteReportRequest request) =>
            _reportService.DeleteReport(request);

        //TODO
        [Route("generate_report_per_time_period"), HttpPost]
        public BaseResponse GenerateReportPerTimePeriod([FromBody]GetResultsForTimePeriodRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var model = new ReportRequest
                { 
                    Text = "",
                    Date = DateTime.Now,
                    Name = $"Звіт за {DateTime.Now}",
                    FamilyId = request.FamilyId,
                    PersonId = request.PersonId
                };

                var allIncomesOrExpensesRequest = new GetIncomesOrExpensesRequest
                {
                    FamilyId = request.FamilyId,
                    PersonId = request.PersonId,
                    Type = "I"
                };
                var allIncomesCount = _changeMoneyService.GetIncomesOrExpenses(allIncomesOrExpensesRequest);
                allIncomesOrExpensesRequest.Type = "E";
                var allExpensesCount = _changeMoneyService.GetIncomesOrExpenses(allIncomesOrExpensesRequest);

                model.Text = $"Кількість доходів: {allIncomesCount.ChangeMoneys.Count()}; Кількість витрат: {allExpensesCount.ChangeMoneys.Count()};";

                var addReportResponse = _reportService.AddReport(model);
                response.IsSuccess = addReportResponse.IsSuccess;
                response.BaseIsSuccess = addReportResponse.BaseIsSuccess;
                response.BaseMessage = addReportResponse.BaseMessage;
                response.ApiMessage = addReportResponse.ApiMessage;
            }
            catch(Exception ex)
            {
                response.BaseIsSuccess = false;
                response.BaseMessage = ex.Message;
            }

            return response;
        }
    }
}
