using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.Models.APIModels;
using WebAPI.Models.APIModels.Requests;
using WebAPI.Models.APIModels.Requests.PersonsControllerRequests;
using WebAPI.Models.APIModels.Responses.PersonsControllerResponses;

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

        [HttpGet]
        [Route("get_persons_by_family_id")]
        public async Task<ActionResult<GetPersonsResponse>> GetPersonsByFamilyIDAsync([FromBody]IdRequest request)
        {
            if(request.FamilyID == null)
            {
                return BadRequest();
            }

            return new GetPersonsResponse
            {
                Persons = await db.Persons.Where(x => x.FamilyId == request.FamilyID).ToListAsync()
            };
        }

        [HttpPost]
        [Route("add_new_person")]
        public async Task<ActionResult<Person>> AddPersonAsync([FromBody] AddNewPersonRequest request)
        {
            if(request.Person.FamilyId == null)
            {
                return BadRequest();
            }

            db.Persons.Add(request.Person);
            await db.SaveChangesAsync();

            return Ok(request.Person);
        }
    }
}
