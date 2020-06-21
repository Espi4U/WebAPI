using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Models.APIModels.Requests;

namespace Shared.Models.Requests
{
    public class GetResultForCurrencyRequest : BaseRequest
    {
        public int CurrencyId { get; set; }
        public string Type { get; set; }
    }
}
