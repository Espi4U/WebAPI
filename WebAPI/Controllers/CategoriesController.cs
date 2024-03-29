﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/v1/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        public CategoriesController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Route("add_category"), HttpPost]
        public BaseResponse AddCategory([FromBody]CategoryRequest request) =>
            _categoryService.AddCategory(request);

        [Route("get_categories"), HttpPost]
        public ListCategoriesResponse GetCategories([FromBody]BaseRequest request) =>
            _categoryService.GetCategories(request);

        [Route("delete_category"), HttpPost]
        public BaseResponse DeleteCategory([FromBody]CategoryRequest request) =>
            _categoryService.DeleteCategory(request);
    }
}
