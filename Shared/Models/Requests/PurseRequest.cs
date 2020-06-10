using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Models.APIModels;
using WebAPI.Models.APIModels.Requests;

namespace Shared.Models.Requests.PursesRequests
{
    public class PurseRequest : BaseRequest
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public int CurrencyId { get; set; }
    }
}
