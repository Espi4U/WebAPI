using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Models.APIModels;

namespace Shared.Models.Responses.PursesResponses
{
    public class ListPursesResponse : BaseResponse
    {
        public List<Purse> Purses { get; set; }
    }
}
