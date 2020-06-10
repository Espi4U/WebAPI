using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Models.APIModels;

namespace Shared.Models.Responses.CategoriesResponses
{
    public class ListCategoriesResponse : BaseResponse
    {
        public List<Category> Categories { get; set; }
    }
}
