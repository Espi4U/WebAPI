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
    [Route("api/v1/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        public CategoriesController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Route("add_new_categoty"), HttpPost]
        public BaseResponse AddCategory([FromBody]CategoryRequest request) =>
            _categoryService.AddCategory(request);

        [Route("get_categories_by_id"), HttpPost]
        public ListCategoriesResponse GetCategories([FromBody]BaseRequest request) =>
            _categoryService.GetCategories(request);

        [Route("delete_category_by_id"), HttpPost]
        public BaseResponse DeleteCategory([FromBody]CategoryRequest request) =>
            _categoryService.DeleteCategory(request);
    }
}
