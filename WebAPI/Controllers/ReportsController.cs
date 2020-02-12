using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.Models.APIModels;
using WebAPI.Models.APIModels.Requests;
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
        public async Task<ActionResult<GetReportsResponse>> GetAllFamilyReportsByID([FromBody]IdRequest request)
        {
            return new GetReportsResponse
            {
                Reports = await db.Reports.Where(x => x.FamilyId == request.Id).ToListAsync()
            };
        }

        [HttpPost]
        [Route("add_new_report")]
        public async Task<ActionResult<Report>> AddNewReportAsync([FromBody]Report report)
        {
            if(report == null)
            {
                return BadRequest();
            }

            db.Reports.Add(report);
            await db.SaveChangesAsync();

            return Ok(report);
        }
    }
}
