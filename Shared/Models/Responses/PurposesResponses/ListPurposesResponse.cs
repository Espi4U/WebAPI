using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Models.APIModels;

namespace Shared.Models.Responses.PurposesResponses
{
    public class ListPurposesResponse : BaseResponse
    {
        public List<Purpose> Purposes { get; set; }
    }
}
