using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Models.Responses
{
    public class DataByCategotyToChartResponse : BaseResponse
    {
        public Dictionary<string, int> Incomes { get; set; }
        public Dictionary<string, int> Expenses { get; set; }
    }
}
