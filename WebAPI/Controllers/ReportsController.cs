using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.Models.APIModels;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        readonly FamilyFinanceContext db;
        public ReportsController(FamilyFinanceContext context)
        {
            db = context;
        }

        [Route("get_all_family_reports_by_id/{familyID:int}")]
        public async Task<ActionResult<IEnumerable<Report>>> GetAllFamilyReportsByID(int familyID)
        {
            return await db.Reports.Where(x => x.FamilyId == familyID).ToListAsync();
        }

        [Route("get_all_person_reports_by_id/{personID:int}")]
        public async Task<ActionResult<IEnumerable<Report>>> GetAllPersonReportsByID(int personID)
        {
            return await db.Reports.Where(x => x.PersonId == personID).ToListAsync();
        }

        [Route("add_new_family_report/{id:int}")]
        public async Task<ActionResult<IEnumerable<Report>>> AddNewFamilyReport([FromQuery]Report report ,int id)
        {
            return await db.Reports.Where(x => x.PersonId == id).ToListAsync();
        }

        [Route("add_new_person_report/{id:int}")]
        public async Task<ActionResult<IEnumerable<Report>>> AddNewPersonReport([FromQuery]Report report, int id)
        {
            return await db.Reports.Where(x => x.PersonId == id).ToListAsync();
        }
    }
}
