using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Models.Responses
{
    public class DataByCategotyToChartResponse : BaseResponse
    {
        public Dictionary<string, int> IncomesUAH { get; set; }
        public Dictionary<string, int> ExpensesUAH { get; set; }
        public Dictionary<string, int> IncomesUSD { get; set; }
        public Dictionary<string, int> ExpensesUSD { get; set; }
        public Dictionary<string, int> IncomesEUR { get; set; }
        public Dictionary<string, int> ExpensesEUR { get; set; }
        public Dictionary<string, int> IncomesPLZ { get; set; }
        public Dictionary<string, int> ExpensesPLZ { get; set; }
    }
}
