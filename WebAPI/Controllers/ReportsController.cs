using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Responses;
using WebAPI.Models;
using WebAPI.Models.APIModels;
using WebAPI.Models.APIModels.Requests;
using WebAPI.Models.APIModels.Requests.ReportsControllerRequests;
using WebAPI.Models.APIModels.Responses;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("reports")]
    public class ReportsController : ControllerBase
    {
        private readonly ReportService _reportService;
        public ReportsController(ReportService reportService)
        {
            _reportService = reportService;
        }

        [Route("get_all_reports_by_id"), HttpPost]
        public ListReportsResponse GetAllReportsById([FromBody]IdRequest request) =>
            _reportService.GetAllReportsById(request);

        [Route("add_new_report"), HttpPost]
        public BaseResponse AddNewReport([FromBody]ReportRequest request) =>
            _reportService.AddNewReport(request);

        [Route("delete_report_by_id"), HttpPost]
        public BaseResponse DeleteReportById([FromBody]ReportRequest request) =>
            _reportService.DeleteReportById(request);
    }
}
