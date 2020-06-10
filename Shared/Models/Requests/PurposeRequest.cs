using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Models.APIModels;
using WebAPI.Models.APIModels.Requests;

namespace Shared.Models.Requests.PurposesRequests
{
    public class PurposeRequest : BaseRequest
    {
        public string Name { get; set; }
        public int FinalSize { get; set; }
        public int CurrencyId { get; set; }
    }
}
