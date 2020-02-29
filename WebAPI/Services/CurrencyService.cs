using Shared.Models.Requests.CurrenciesRequests;
using Shared.Models.Responses;
using Shared.Models.Responses.CurrenciesResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Models.APIModels.Requests;

namespace WebAPI.Services
{
    public class CurrencyService : BaseService
    {
        public BaseResponse AddNewCurrency(CurrencyRequest request)
        {
            return GetResponse(() => {
                var response = new BaseResponse();
                try
                {
                    using (FamilyFinanceContext db = new FamilyFinanceContext())
                    {
                        if (request.Currency == null)
                        {
                            response.BaseIsSuccess = false;
                            response.BaseMessage = "Cannot add empty currency";
                        }
                        else if (db.Currencies.Contains(request.Currency))
                        {
                            response.BaseIsSuccess = false;
                            response.BaseMessage = "Currency is already exist";
                        }
                        else
                        {
                            db.Currencies.Add(request.Currency);
                            db.SaveChanges();
                        }
                    }

                    return response;
                }
                catch
                {
                    response.BaseIsSuccess = false;
                    response.BaseMessage = "Bad request";
                    return response;
                }
            });
        }

        public ListCurrenciesResponse GetCurrenciesById(IdRequest request)
        {
            return GetResponse(() => {
                var response = new ListCurrenciesResponse();
                try
                {
                    using (FamilyFinanceContext db = new FamilyFinanceContext())
                    {
                        if (request.PersonId == null && request.FamilyId == null)
                        {
                            response.BaseIsSuccess = false;
                            response.BaseMessage = "Bad request";

                            return response;
                        }

                        response.Currencies = request.PersonId == null ? db.Currencies.Where(x => x.FamilyId == request.FamilyId).ToList() :
                            db.Currencies.Where(x => x.PersonId == request.PersonId).ToList();
                    }

                    return response;
                }
                catch
                {
                    response.BaseIsSuccess = false;
                    response.BaseMessage = "Bad request";
                    return response;
                }
            });
        }

        public BaseResponse DeleteCurrencyById(CurrencyRequest request)
        {
            return GetResponse(() => {
                var response = new BaseResponse();
                try
                {
                    using (FamilyFinanceContext db = new FamilyFinanceContext())
                    {
                        var currency = db.Currencies.Where(x => x.Id == request.Currency.Id).FirstOrDefault();

                        db.Currencies.Remove(currency);
                        db.SaveChanges();
                    }

                    return response;
                }
                catch
                {
                    response.BaseIsSuccess = false;
                    response.BaseMessage = "Bad request";
                    return response;
                }
            });
        }
    }
}
