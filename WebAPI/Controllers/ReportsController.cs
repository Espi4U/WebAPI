using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shared.Models.Requests.BaseRequests;
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
        public BaseResponse DeleteReportById([FromBody]ReportRequest request) =>
            _reportService.DeleteReport(request);

        //TODO
        [Route("generate_report_per_time_period"), HttpPost]
        public ReportResponse GenerateReportPerTimePeriod([FromBody]GetResultsForTimePeriodRequest request)
        {
            var response = new ReportResponse();

            return response;
        }
    }
}
