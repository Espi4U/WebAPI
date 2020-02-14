using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        readonly FamilyFinanceContext db;
        public ReportsController(FamilyFinanceContext context)
        {
            db = context;
        }

        [HttpGet]
        [Route("get_all_reports")]
        public async Task<ActionResult<GetReportsResponse>> GetAllReportsByIDAsync([FromBody]IdRequest request)
        {
            if(request.FamilyID == null && request.PersonID == null)
            {
                return BadRequest();
            }

            return new GetReportsResponse
            {
                Reports = request.FamilyID == null ? await db.Reports.Where(x => x.PersonId == request.PersonID).ToListAsync() : await db.Reports.Where(x => x.FamilyId == request.FamilyID).ToListAsync()
            };
        }

        [HttpPost]
        [Route("add_new_report")]
        public async Task<ActionResult<Report>> AddNewReportAsync([FromBody]AddNewReportRequest request)
        {
            if(request.Report.FamilyId == null && request.Report.PersonId == null)
            {
                return BadRequest();
            }

            db.Reports.Add(request.Report);
            await db.SaveChangesAsync();

            return Ok(request.Report);
        }
    }
}
