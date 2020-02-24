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

namespace WebAPI.Controllers
{
    [Route("reports")]
    public class ReportsController : ControllerBase
    {
        private readonly FamilyFinanceContext db;
        public ReportsController(FamilyFinanceContext context)
        {
            db = context;
        }

        [HttpGet]
        [Route("get_all_reports_by_id")]
        public async Task<ListReportsResponse> GetAllReportsByIdAsync([FromBody]IdRequest request)
        {
            var response = new ListReportsResponse();
            try
            {
                if (request.PersonId == null && request.FamilyId == null)
                {
                    response.BaseIsSuccess = false;
                    response.BaseMessage = "Bad request";

                    return response;
                }

                response.Reports = request.PersonId == null ? await db.Reports.Where(x => x.FamilyId == request.FamilyId).ToListAsync() :
                    await db.Reports.Where(x => x.PersonId == request.PersonId).ToListAsync();
                return response;
            }
            catch
            {
                response.BaseIsSuccess = false;
                response.BaseMessage = "Bad request";
                return response;
            }
        }

        [HttpPost]
        [Route("add_new_report")]
        public async Task<BaseResponse> AddNewReportAsync([FromBody]ReportRequest request)
        {
            var response = new BaseResponse();
            try
            {
                if (request.Report == null)
                {
                    response.BaseIsSuccess = false;
                    response.BaseMessage = "Cannot add empty report";
                    return response;
                }
                if (db.Reports.Contains(request.Report))
                {
                    response.BaseIsSuccess = false;
                    response.BaseMessage = "Report is already exist";
                    return response;
                }

                db.Reports.Add(request.Report);
                await db.SaveChangesAsync();

                return response;
            }
            catch
            {
                response.BaseIsSuccess = false;
                response.BaseMessage = "Bad request";
                return response;
            }
        }

        [HttpGet]
        [Route("delete_report_by_id")]
        public async Task<BaseResponse> DeleteReportByIdAsync([FromBody]ReportRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var report = await db.Reports.Where(x => x.Id == request.Report.Id).FirstOrDefaultAsync();

                db.Reports.Remove(report);
                await db.SaveChangesAsync();

                return response;
            }
            catch
            {
                response.BaseIsSuccess = false;
                response.BaseMessage = "Bad request";
                return response;
            }
        }
    }
}
