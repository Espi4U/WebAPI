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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Family>>> Get()
        {
            return await db.Families.ToListAsync();
        }

        [HttpPost]
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

        [HttpPut]
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

        [HttpDelete("{id}")]
        public async Task<ActionResult<Family>> Delete(int id)
        {
            Family user = db.Families.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            db.Families.Remove(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }
    }
}
