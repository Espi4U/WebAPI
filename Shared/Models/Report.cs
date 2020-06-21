using Newtonsoft.Json;
using System;

namespace WebAPI.Models.APIModels
{
    public class Report
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public int IncomesCount { get; set; }
        public int ExpensesCount { get; set; }
        public int SumUAHIncomes { get; set; }
        public int SumUSDIncomes { get; set; }
        public int SumEURIncomes { get; set; }
        public int SumPLZIncomes { get; set; }
        public int SumUAHExpenses { get; set; }
        public int SumUSDExpenses { get; set; }
        public int SumEURExpenses { get; set; }
        public int SumPLZExpenses { get; set; }
        public string DescriptionLine1 { get; set; }
        public string DescriptionLine2 { get; set; }
        public DateTime Date { get; set; }
        public int? FamilyId { get; set; }
        public Family Family { get; set; }
        public int? PersonId { get; set; }
        public Person Person { get; set; }
    }
}