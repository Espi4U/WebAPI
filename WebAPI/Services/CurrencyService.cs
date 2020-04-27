﻿using Shared.Models.Requests.CurrenciesRequests;
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
        public BaseResponse AddCurrency(CurrencyRequest request)
        {
            return GetResponse(() => {
                var response = new BaseResponse();
                try
                {
                    using (FamilyFinanceContext db = new FamilyFinanceContext())
                    {
                        if (request.Currency == default)
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
                }
                catch
                {
                    response.BaseIsSuccess = false;
                    response.BaseMessage = "Bad request";
                }

                return response;
            });
        }

        public ListCurrenciesResponse GetCurrencies(BaseRequest request)
        {
            return GetResponse(() => {
                var response = new ListCurrenciesResponse();
                try
                {
                    using (FamilyFinanceContext db = new FamilyFinanceContext())
                    {
                        response.Currencies = db.Currencies.ToList();
                    }
                }
                catch
                {
                    response.BaseIsSuccess = false;
                    response.BaseMessage = "Bad request";
                }

                return response;
            });
        }

        public BaseResponse DeleteCurrency(CurrencyRequest request)
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
                }
                catch
                {
                    response.BaseIsSuccess = false;
                    response.BaseMessage = "Bad request";
                }

                return response;
            });
        }
    }
}
