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
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        public CategoriesController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Route("add_new_categoty"), HttpPost]
        public BaseResponse AddNewCategory([FromBody]CategoryRequest request) =>
            _categoryService.AddNewCategory(request);

        [Route("get_categories_by_id"), HttpPost]
        public ListCategoriesResponse GetCategoriesById([FromBody]IdRequest request) =>
            _categoryService.GetCategoriesById(request);

        [Route("delete_category_by_id"), HttpPost]
        public BaseResponse DeleteCategoryById([FromBody]CategoryRequest request) =>
            _categoryService.DeleteCategoryById(request);
    }
}
