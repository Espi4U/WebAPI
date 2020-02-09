using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.Models.APIModels;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonsController : ControllerBase
    {
        readonly FamilyFinanceContext db;
        public PersonsController(FamilyFinanceContext context)
        {
            db = context;
        }

        [Route("get_all_persons_by_family_id/{id:int}")]
        public async Task<ActionResult<IEnumerable<Person>>> GetAllPersonsByFamilyID(int id)
        {
            return await db.Persons.Where(x => x.FamilyId == id).ToListAsync();
        }
    }
}
