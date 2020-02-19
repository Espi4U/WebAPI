using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Models.APIModels;

namespace Shared.Models.Requests.CategoriesRequests
{
    public class CategoryRequest
    {
        [JsonProperty("category")]
        public Category Category { get; set; }
    }
}
