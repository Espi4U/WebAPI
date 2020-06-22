using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shared.Models.Requests;
using Shared.Models.Requests.BaseRequests;
using Shared.Models.Requests.ChangeMoneyRequests;
using Shared.Models.Responses;
using Shared.Models.Responses.ReportsResponses;
using WebAPI.Models;
using WebAPI.Models.APIModels;
using WebAPI.Models.APIModels.Requests;
using WebAPI.Models.APIModels.Requests.ReportsControllerRequests;
using WebAPI.Models.APIModels.Responses;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/v1/reports")]
    public class ReportsController : ControllerBase
    {
        private readonly ReportService _reportService;
        private readonly ChangeMoneyService _changeMoneyService;
        private readonly PersonService _personService;
        public ReportsController(
            ReportService reportService,
            ChangeMoneyService changeMoneyService,
            PersonService personService)
        {
            _personService = personService;
            _reportService = reportService;
            _changeMoneyService = changeMoneyService;
        }

        [Route("get_reports"), HttpPost]
        public ListReportsResponse GetAllReportsById([FromBody]BaseRequest request) =>
            _reportService.GetReports(request);


        [Route("delete_report"), HttpPost]
        public BaseResponse DeleteReportById([FromBody]DeleteReportRequest request) =>
            _reportService.DeleteReport(request);

        [Route("generate_report_per_time_period"), HttpPost]
        public BaseResponse GenerateReportPerTimePeriod([FromBody]GetResultsForTimePeriodRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var familyName = _personService.GetName((int)request.FamilyId, 0);
                var personName = _personService.GetName((int)request.PersonId, 1);

                var model = new ReportRequest
                { 
                    Text = @"",
                    Date = request.Now,
                    Name = $"Звіт за {DateTime.Now}, користуача {personName.Name}",
                    FamilyId = request.FamilyId,
                    PersonId = request.PersonId
                };

                var allIncomesOrExpensesRequest = new GetResultsForTimePeriodRequest
                {
                    FamilyId = request.FamilyId,
                    PersonId = request.PersonId,
                    Type = "I",
                    Start = request.Start,
                    End = request.End
                };
                var allIncomesCount = _changeMoneyService.GetResultForTimePeriod(allIncomesOrExpensesRequest);
                allIncomesOrExpensesRequest.Type = "E";
                var allExpensesCount = _changeMoneyService.GetResultForTimePeriod(allIncomesOrExpensesRequest);

                var requestForCurrency = new GetResultForCurrencyRequest
                {
                    FamilyId = request.FamilyId,
                    PersonId = request.PersonId,
                    Type = "I",
                    CurrencyId = 1,
                    Start = request.Start,
                    End = request.End
                };
                var incomesUAH = _changeMoneyService.GetResultForCurrency(requestForCurrency);
                requestForCurrency.CurrencyId = 2;
                var incomesUSD = _changeMoneyService.GetResultForCurrency(requestForCurrency);
                requestForCurrency.CurrencyId = 3;
                var incomesEUR = _changeMoneyService.GetResultForCurrency(requestForCurrency);
                requestForCurrency.CurrencyId = 4;
                var incomesPLZ = _changeMoneyService.GetResultForCurrency(requestForCurrency);

                requestForCurrency.Type = "E";
                requestForCurrency.CurrencyId = 1;

                var expensesUAH = _changeMoneyService.GetResultForCurrency(requestForCurrency);
                requestForCurrency.CurrencyId = 2;
                var expensesUSD = _changeMoneyService.GetResultForCurrency(requestForCurrency);
                requestForCurrency.CurrencyId = 3;
                var expensesEUR = _changeMoneyService.GetResultForCurrency(requestForCurrency);
                requestForCurrency.CurrencyId = 4;
                var expensesPLZ = _changeMoneyService.GetResultForCurrency(requestForCurrency);

                var template = @"<html>
                                   <body style='background-color: lightgray;'>
                                      <h1>Звіт</h1>
                                      <h4>{0}</h4>
                                      <p>Кількість витрат: {1}</p>
                                      <p>Кількість доходів: {2}</p><hr>
                                      <p>Кількість витрат у гривнях: {3}</p>
                                      <p>Кількість доходів у гривнях: {4}</p><hr>
                                      <p>Кількість витрат у доларах США: {5}</p>
                                      <p>Кількість доходів у доларах США: {6}</p><hr>
                                      <p>Кількість витрат у євро: {7}</p>
                                      <p>Кількість доходів у євро: {8}</p><hr>
                                      <p>Кількість витрат у злотих: {9}</p>
                                      <p>Кількість доходів у злотих: {10}</p><hr>
                                      <h3>Найбільші доходи по валютах</h3>
                                      <p>Українська Гривня - {11}({12} UAH)</p>
                                      <p>Долар США - {13}({14} USD)</p>
                                      <p>Євро - {15}({16} EUR)</p>
                                      <p>Польський Злотий - {17}({18} PLZ)</p><hr>
                                      <h3>Найбільші витрати по валютах</h3>
                                      <p>Українська Гривня - {19}({20} UAH)</p>
                                      <p>Долар США - {21}({22} USD)</p>
                                      <p>Євро - {23}({24} EUR)</p>
                                      <p>Польський Злотий - {25}({26} PLZ)</p><hr>
                                      <h3>Найменші доходи по валютах</h3>
                                      <p>Українська Гривня - {27}({28} UAH)</p>
                                      <p>Долар США - {29}({30} USD)</p>
                                      <p>Євро - {31}({32} EUR)</p>
                                      <p>Польський Злотий - {33}({34} PLZ)</p><hr>
                                      <h3>Найменші витрати по валютах</h3>
                                      <p>Українська Гривня - {35}({36} UAH)</p>
                                      <p>Долар США - {37}({38} USD)</p>
                                      <p>Євро - {39}({40} EUR)</p>
                                      <p>Польський Злотий - {41}({42} PLZ)</p><hr>
                                  </body>
                              </html>";

                model.Text = String.Format(template,
                    $"Сім'я: {familyName.Name}; Користувач: {personName.Name};",
                    allExpensesCount.ChangeMoneys.Count(),
                    allIncomesCount.ChangeMoneys.Count(),
                    expensesUAH.ChangeMoneys.Count(),
                    incomesUAH.ChangeMoneys.Count(),
                    expensesUSD.ChangeMoneys.Count(),
                    incomesUSD.ChangeMoneys.Count(),
                    expensesEUR.ChangeMoneys.Count(),
                    incomesEUR.ChangeMoneys.Count(),
                    expensesPLZ.ChangeMoneys.Count(),
                    incomesPLZ.ChangeMoneys.Count(),
                    incomesUAH.ChangeMoneys.Where(x => x.Size == incomesUAH.ChangeMoneys.Max(x => x.Size)).FirstOrDefault() != null ? incomesUAH.ChangeMoneys.Where(x => x.Size == incomesUAH.ChangeMoneys.Max(x => x.Size)).FirstOrDefault().Name : "",
                    incomesUAH.ChangeMoneys.Count() != 0 ? incomesUAH.ChangeMoneys.Max(x => x.Size) : 0,
                    incomesUSD.ChangeMoneys.Where(x => x.Size == incomesUSD.ChangeMoneys.Max(x => x.Size)).FirstOrDefault() != null ? incomesUSD.ChangeMoneys.Where(x => x.Size == incomesUSD.ChangeMoneys.Max(x => x.Size)).FirstOrDefault().Name : "",
                    incomesUSD.ChangeMoneys.Count() != 0 ? incomesUSD.ChangeMoneys.Max(x => x.Size) : 0,
                    incomesEUR.ChangeMoneys.Where(x => x.Size == incomesEUR.ChangeMoneys.Max(x => x.Size)).FirstOrDefault() != null ? incomesEUR.ChangeMoneys.Where(x => x.Size == incomesEUR.ChangeMoneys.Max(x => x.Size)).FirstOrDefault().Name : "",
                    incomesEUR.ChangeMoneys.Count() != 0 ? incomesEUR.ChangeMoneys.Max(x => x.Size) : 0,
                    incomesPLZ.ChangeMoneys.Where(x => x.Size == incomesPLZ.ChangeMoneys.Max(x => x.Size)).FirstOrDefault() != null ? incomesPLZ.ChangeMoneys.Where(x => x.Size == incomesPLZ.ChangeMoneys.Max(x => x.Size)).FirstOrDefault().Name : "",
                    incomesPLZ.ChangeMoneys.Count() != 0 ? incomesPLZ.ChangeMoneys.Max(x => x.Size) : 0,
                    expensesUAH.ChangeMoneys.Where(x => x.Size == expensesUAH.ChangeMoneys.Max(x => x.Size)).FirstOrDefault() != null ? expensesUAH.ChangeMoneys.Where(x => x.Size == expensesUAH.ChangeMoneys.Max(x => x.Size)).FirstOrDefault().Name : "",
                    expensesUAH.ChangeMoneys.Count() != 0 ? expensesUAH.ChangeMoneys.Max(x => x.Size) : 0,
                    expensesUSD.ChangeMoneys.Where(x => x.Size == expensesUSD.ChangeMoneys.Max(x => x.Size)).FirstOrDefault() != null ? expensesUSD.ChangeMoneys.Where(x => x.Size == expensesUSD.ChangeMoneys.Max(x => x.Size)).FirstOrDefault().Name : "",
                    expensesUSD.ChangeMoneys.Count() != 0 ? expensesUSD.ChangeMoneys.Max(x => x.Size) : 0,
                    expensesEUR.ChangeMoneys.Where(x => x.Size == expensesEUR.ChangeMoneys.Max(x => x.Size)).FirstOrDefault() != null ? expensesEUR.ChangeMoneys.Where(x => x.Size == expensesEUR.ChangeMoneys.Max(x => x.Size)).FirstOrDefault().Name : "",
                    expensesEUR.ChangeMoneys.Count() != 0 ? expensesEUR.ChangeMoneys.Max(x => x.Size) : 0,
                    expensesPLZ.ChangeMoneys.Where(x => x.Size == expensesPLZ.ChangeMoneys.Max(x => x.Size)).FirstOrDefault() != null ? expensesPLZ.ChangeMoneys.Where(x => x.Size == expensesPLZ.ChangeMoneys.Max(x => x.Size)).FirstOrDefault().Name : "",
                    expensesPLZ.ChangeMoneys.Count() != 0 ? expensesPLZ.ChangeMoneys.Max(x => x.Size) : 0,
                    incomesUAH.ChangeMoneys.Where(x => x.Size == incomesUAH.ChangeMoneys.Min(x => x.Size)).FirstOrDefault() != null ? incomesUAH.ChangeMoneys.Where(x => x.Size == incomesUAH.ChangeMoneys.Min(x => x.Size)).FirstOrDefault().Name : "",
                    incomesUAH.ChangeMoneys.Count() != 0 ? incomesUAH.ChangeMoneys.Min(x => x.Size) : 0,
                    incomesUSD.ChangeMoneys.Where(x => x.Size == incomesUSD.ChangeMoneys.Min(x => x.Size)).FirstOrDefault() != null ? incomesUSD.ChangeMoneys.Where(x => x.Size == incomesUSD.ChangeMoneys.Min(x => x.Size)).FirstOrDefault().Name : "",
                    incomesUSD.ChangeMoneys.Count() != 0 ? incomesUSD.ChangeMoneys.Min(x => x.Size) : 0,
                    incomesEUR.ChangeMoneys.Where(x => x.Size == incomesEUR.ChangeMoneys.Min(x => x.Size)).FirstOrDefault() != null ? incomesEUR.ChangeMoneys.Where(x => x.Size == incomesEUR.ChangeMoneys.Min(x => x.Size)).FirstOrDefault().Name : "",
                    incomesEUR.ChangeMoneys.Count != 0 ? incomesEUR.ChangeMoneys.Min(x => x.Size) : 0,
                    incomesPLZ.ChangeMoneys.Where(x => x.Size == incomesPLZ.ChangeMoneys.Min(x => x.Size)).FirstOrDefault() != null ? incomesPLZ.ChangeMoneys.Where(x => x.Size == incomesPLZ.ChangeMoneys.Min(x => x.Size)).FirstOrDefault().Name : "",
                    incomesPLZ.ChangeMoneys.Count() != 0 ? incomesPLZ.ChangeMoneys.Min(x => x.Size) : 0,
                    expensesUAH.ChangeMoneys.Where(x => x.Size == expensesUAH.ChangeMoneys.Min(x => x.Size)).FirstOrDefault() != null ? expensesUAH.ChangeMoneys.Where(x => x.Size == expensesUAH.ChangeMoneys.Min(x => x.Size)).FirstOrDefault().Name : "",
                    expensesUAH.ChangeMoneys.Count() != 0 ? expensesUAH.ChangeMoneys.Min(x => x.Size) : 0,
                    expensesUSD.ChangeMoneys.Where(x => x.Size == expensesUSD.ChangeMoneys.Min(x => x.Size)).FirstOrDefault() != null ? expensesUSD.ChangeMoneys.Where(x => x.Size == expensesUSD.ChangeMoneys.Min(x => x.Size)).FirstOrDefault().Name : "",
                    expensesUSD.ChangeMoneys.Count() != 0 ? expensesUSD.ChangeMoneys.Min(x => x.Size) : 0,
                    expensesEUR.ChangeMoneys.Where(x => x.Size == expensesEUR.ChangeMoneys.Min(x => x.Size)).FirstOrDefault() != null ? expensesEUR.ChangeMoneys.Where(x => x.Size == expensesEUR.ChangeMoneys.Min(x => x.Size)).FirstOrDefault().Name : "",
                    expensesEUR.ChangeMoneys.Count() != 0 ? expensesEUR.ChangeMoneys.Min(x => x.Size) : 0,
                    expensesPLZ.ChangeMoneys.Where(x => x.Size == expensesPLZ.ChangeMoneys.Min(x => x.Size)).FirstOrDefault() != null ? expensesPLZ.ChangeMoneys.Where(x => x.Size == expensesPLZ.ChangeMoneys.Min(x => x.Size)).FirstOrDefault().Name : "",
                    expensesPLZ.ChangeMoneys.Count() != 0 ? expensesPLZ.ChangeMoneys.Min(x => x.Size) : 0);

                var addReportResponse = _reportService.AddReport(model);
                response.IsSuccess = addReportResponse.IsSuccess;
                response.BaseIsSuccess = addReportResponse.BaseIsSuccess;
                response.BaseMessage = addReportResponse.BaseMessage;
                response.ApiMessage = addReportResponse.ApiMessage;
            }
            catch(Exception ex)
            {
                response.BaseIsSuccess = false;
                response.BaseMessage = ex.Message;
            }

            return response;
        }
    }
}
