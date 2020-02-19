using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Requests.CategoriesRequests;
using Shared.Models.Responses;
using Shared.Models.Responses.CategoriesResponses;
using WebAPI.Models;
using WebAPI.Models.APIModels;
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

        [HttpPost]
        [Route("add_new_categoty")]
        public async Task<ActionResult<BaseResponse>> AddNewCategoryAsync([FromBody]CategoryRequest request)
        {
            var response = new BaseResponse();

            if (request.Category == null)
            {
                response.IsError = true;
                response.Message = "Cannot add empty category";
            }

            if (db.Categories.Contains(request.Category))
            {
                response.IsError = true;
                response.Message = "Categoty is already exist";
            }

            db.Categories.Add(request.Category);
            await db.SaveChangesAsync();

            return response;
        }

        [HttpGet]
        [Route("add_new_categoty")]
        public async Task<ActionResult<ListCategoriesResponse>> GetAllCategoriesById([FromBody]IdRequest request)
        {
            var response = new ListCategoriesResponse();

            //if (request.Category == null)
            //{
            //    response.IsError = true;
            //    response.Message = "Cannot add empty category";
            //}

            //if (db.Categories.Contains(request.Category))
            //{
            //    response.IsError = true;
            //    response.Message = "Categoty is already exist";
            //}

            //db.Categories.Add(request.Category);
            //await db.SaveChangesAsync();

            return response;
        }
    }
}
