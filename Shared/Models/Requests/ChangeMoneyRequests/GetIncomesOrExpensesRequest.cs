using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Models.APIModels.Requests;

namespace Shared.Models.Requests.ChangeMoneyRequests
{
    public class GetIncomesOrExpensesRequest : BaseRequest
    {
        public string Type { get; set; }
    }
}
