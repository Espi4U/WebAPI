using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Models.APIModels;
using WebAPI.Models.APIModels.Requests;

namespace Shared.Models.Requests.CurrenciesRequests
{
    public class CurrencyRequest : IdRequest
    {
        [JsonProperty("currency")]
        public Currency Currency { get; set; }
    }
}
