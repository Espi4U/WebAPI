using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Requests.CurrenciesRequests;
using Shared.Models.Responses;
using Shared.Models.Responses.CurrenciesResponses;
using WebAPI.Models;
using WebAPI.Models.APIModels.Requests;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("currencies")]
    public class CurrenciesController : ControllerBase
    {
        private readonly FamilyFinanceContext db;
        public CurrenciesController(FamilyFinanceContext context)
        {
            db = context;
        }

        [HttpPost]
        [Route("add_new_currency")]
        public async Task<BaseResponse> AddNewCurrencyAsync([FromBody]CurrencyRequest request)
        {
            var response = new BaseResponse();
            try
            {
                if (request.Currency == null)
                {
                    response.IsError = true;
                    response.Message = "Cannot add empty currency";
                    return response;
                }
                if (db.Currencies.Contains(request.Currency))
                {
                    response.IsError = true;
                    response.Message = "Currency is already exist";
                    return response;
                }

                db.Currencies.Add(request.Currency);
                await db.SaveChangesAsync();

                return response;
            }
            catch
            {
                response.IsError = true;
                response.Message = "Bad request";
                return response;
            }
        }

        [HttpGet]
        [Route("get_all_currencies_by_id")]
        public async Task<ListCurrenciesResponse> GetAllCurrenciesByIdAsync([FromBody]IdRequest request)
        {
            var response = new ListCurrenciesResponse();
            try
            {
                if (request.PersonId == null && request.FamilyId == null)
                {
                    response.IsError = true;
                    response.Message = "Bad request";

                    return response;
                }

                response.Currencies = request.PersonId == null ? await db.Currencies.Where(x => x.FamilyId == request.FamilyId).ToListAsync() :
                    await db.Currencies.Where(x => x.PersonId == request.PersonId).ToListAsync();
                return response;
            }
            catch
            {
                response.IsError = true;
                response.Message = "Bad request";
                return response;
            }
        }

        [HttpGet]
        [Route("delete_currency_by_id")]
        public async Task<BaseResponse> DeleteCurrencyByIdAsync([FromBody]CurrencyRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var report = await db.Currencies.Where(x => x.Id == request.Currency.Id).FirstOrDefaultAsync();

                db.Currencies.Remove(report);
                await db.SaveChangesAsync();

                return response;
            }
            catch
            {
                response.IsError = true;
                response.Message = "Bad request";
                return response;
            }
        }
    }
}
