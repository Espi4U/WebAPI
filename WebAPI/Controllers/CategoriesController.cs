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

        [HttpPost]
        [Route("add_new_categoty")]
        public async Task<BaseResponse> AddNewCategoryAsync([FromBody]CategoryRequest request)
        {
            var response = new BaseResponse();
            try
            {
                if (request.Category == null)
                {
                    response.IsError = true;
                    response.Message = "Cannot add empty category";
                    return response;
                }
                if (db.Categories.Contains(request.Category))
                {
                    response.IsError = true;
                    response.Message = "Categoty is already exist";
                    return response;
                }

                db.Categories.Add(request.Category);
                await db.SaveChangesAsync();

                return response;
            }
            catch
            {
                response.IsError = true;
                response.Message = "Bad request";
                return response;
            }
        }

        [HttpGet]
        [Route("get_categories_by_id")]
        public async Task<ListCategoriesResponse> GetAllCategoriesByIdAsync([FromBody]IdRequest request)
        {
            var response = new ListCategoriesResponse();
            try
            {
                if (request.PersonId == null && request.FamilyId == null)
                {
                    response.IsError = true;
                    response.Message = "Bad request";

                    return response;
                }

                response.Categories = request.PersonId == null ? await db.Categories.Where(x => x.FamilyId == request.FamilyId).ToListAsync() :
                    await db.Categories.Where(x => x.PersonId == request.PersonId).ToListAsync();
                return response;
            }
            catch
            {
                response.IsError = true;
                response.Message = "Bad request";
                return response;
            }
        }

        [HttpGet]
        [Route("delete_category_by_id")]
        public async Task<BaseResponse> DeleteCategoryByIdAsync([FromBody]CategoryRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var category = await db.Categories.Where(x => x.Id == request.Category.Id).FirstOrDefaultAsync();

                db.Categories.Remove(category);
                await db.SaveChangesAsync();

                return response;
            }
            catch
            {
                response.IsError = true;
                response.Message = "Bad request";
                return response;
            }
        }
    }
}
