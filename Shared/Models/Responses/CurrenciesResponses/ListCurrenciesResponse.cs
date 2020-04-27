using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Models.APIModels;

namespace Shared.Models.Responses.CurrenciesResponses
{
    public class ListCurrenciesResponse : BaseResponse
    {
        public List<Currency> Currencies { get; set; }
    }
}
