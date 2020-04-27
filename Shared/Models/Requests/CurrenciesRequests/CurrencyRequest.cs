using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Models.APIModels;
using WebAPI.Models.APIModels.Requests;

namespace Shared.Models.Requests.CurrenciesRequests
{
    public class CurrencyRequest
    {
        public Currency Currency { get; set; }
    }
}
