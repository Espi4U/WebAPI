using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.Models.APIModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FamiliesController : ControllerBase
    {
        readonly FamilyFinanceContext db;
        public FamiliesController(FamilyFinanceContext context)
        {
            db = context;
        }

        [HttpGet("get_all_families")]
        public async Task<ActionResult<IEnumerable<Family>>> GetAllFamilies()
        {
            return await db.Families.ToListAsync();
        }

        [HttpGet("get_all_persons_by_familyid/{id:int}")]
        public async Task<ActionResult<IEnumerable<Family>>> GetAllPersonsByFamilyID(int id)
        {
            return await db.Families.Where(f=>f.Id == id).Include(p=>p.Persons).ToListAsync();
        }

        [HttpPost("add_new_family")]
        public async Task<ActionResult<Family>> Post(Family family)
        {
            if (family == null)
            {
                return BadRequest();
            }

            db.Families.Add(family);
            await db.SaveChangesAsync();
            return Ok(family);
        }

        [HttpPut("update_family")]
        public async Task<ActionResult<Family>> Put(Family family)
        {
            if (family == null)
            {
                return BadRequest();
            }
            if (!db.Families.Any(x => x.Id == family.Id))
            {
                return NotFound();
            }

            db.Update(family);
            await db.SaveChangesAsync();
            return Ok(family);
        }

        [Route("delete_family/{id:int}")]
        public async Task<ActionResult<Family>> Delete(int id)
        {
            Family family = db.Families.FirstOrDefault(x => x.Id == id);
            if (family == null)
            {
                return NotFound();
            }

            family.Persons.Clear();
            family.Purposes.Clear();
            family.Purses.Clear();
            family.Reports.Clear();
            family.ChangesInMoney.Clear();

            db.Families.Remove(family);
            await db.SaveChangesAsync();
            return Ok(family);
        }
    }
}
