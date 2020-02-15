using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Responses.CategoriesResponses;
using WebAPI.Models;
using WebAPI.Models.APIModels.Requests;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly FamilyFinanceContext db;
        public CategoriesController(FamilyFinanceContext context)
        {
            db = context;
        }

        [HttpGet]
        [Route("get_all_categories")]
        public async Task<ActionResult<ListCategoriesResponse>> GetAllCategoriesAsync([FromBody]IdRequest request)
        {
            if(request.PersonID == null && request.FamilyID == null)
            {
                return BadRequest();
            }

            return new ListCategoriesResponse
            {
                Categories = await db.Categories.ToListAsync()
            };
        }
    }
}
