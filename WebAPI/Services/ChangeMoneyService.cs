﻿using Shared.Models.Requests.BaseRequests;
using Shared.Models.Requests.ChangeMoneyRequests;
using Shared.Models.Responses;
using Shared.Models.Responses.ChangeMoneysResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Models.APIModels;
using WebAPI.Models.APIModels.Requests;

namespace WebAPI.Services
{
    public class ChangeMoneyService : BaseService
    {
        public ListChangeMoneysResponse GetIncomesOrExpenses(GetIncomesOrExpensesRequest request)
        {
            return GetResponse(() => {
                var response = new ListChangeMoneysResponse();
                try
                {
                    using (FamilyFinanceContext db = new FamilyFinanceContext())
                    {
                        if(request.FamilyId == null && request.PersonId == null)
                        {
                            response.BaseMessage = Shared.Constants.NEED_AUTHORIZE;
                            response.IsSuccess = false;
                        }
                        else
                        {
                            if(request.PersonId != null || request.FamilyId != null)
                            {
                                response.ChangeMoneys = db.ChangeMoneys.Where(x => x.Type == request.Type && (x.FamilyId == request.FamilyId || x.PersonId == request.PersonId)).ToList();
                            }
                            else
                            {
                                response.ChangeMoneys = request.FamilyId == null ?
                                db.ChangeMoneys.Where(x => x.PersonId == request.PersonId && x.Type == request.Type).ToList():
                                db.ChangeMoneys.Where(x => x.FamilyId == request.FamilyId && x.Type == request.Type).ToList();
                            }
                        }
                    }
                }
                catch
                {
                    response.BaseIsSuccess = false;
                    response.BaseMessage = Shared.Constants.BAD_REQUEST;
                }

                return response;
            });
        }

        public ChangeMoneyResponse GetLargestIncomeOrExpense(GetIncomesOrExpensesRequest request)
        {
            return GetResponse(() => {
                var response = new ChangeMoneyResponse();
                try
                {
                    using (FamilyFinanceContext db = new FamilyFinanceContext())
                    {
                        if (request.FamilyId == null && request.PersonId == null)
                        {
                            response.BaseMessage = Shared.Constants.NEED_AUTHORIZE;
                            response.IsSuccess = false;
                        }
                        else
                        {
                            if (request.PersonId != null || request.FamilyId != null)
                            {
                                response.ChangeMoney = db.ChangeMoneys.Where(x => x.Type == request.Type && (x.FamilyId == request.FamilyId || x.PersonId == request.PersonId)).Max();
                            }
                            else
                            {
                                response.ChangeMoney = request.FamilyId == null ?
                                db.ChangeMoneys.Where(x => x.PersonId == request.PersonId && x.Type == request.Type).Max():
                                db.ChangeMoneys.Where(x => x.FamilyId == request.FamilyId && x.Type == request.Type).Max();
                            }
                        }
                    }
                }
                catch
                {
                    response.BaseIsSuccess = false;
                    response.BaseMessage = Shared.Constants.BAD_REQUEST;
                }

                return response;
            });
        }

        public ChangeMoneyResponse GetSmallestIncomeOrExpense(GetIncomesOrExpensesRequest request)
        {
            return GetResponse(() => {
                var response = new ChangeMoneyResponse();
                try
                {
                    using (FamilyFinanceContext db = new FamilyFinanceContext())
                    {
                        if (request.FamilyId == null && request.PersonId == null)
                        {
                            response.BaseMessage = Shared.Constants.BAD_REQUEST;
                            response.IsSuccess = false;
                        }
                        else
                        {
                            if (request.PersonId != null || request.FamilyId != null)
                            {
                                response.ChangeMoney = db.ChangeMoneys.Where(x => x.Type == request.Type && (x.FamilyId == request.FamilyId || x.PersonId == request.PersonId)).Min();
                            }
                            else
                            {
                                response.ChangeMoney = request.FamilyId == null ?
                                db.ChangeMoneys.Where(x => x.PersonId == request.PersonId && x.Type == request.Type).Min():
                                db.ChangeMoneys.Where(x => x.FamilyId == request.FamilyId && x.Type == request.Type).Min();
                            }
                        }
                    }
                }
                catch
                {
                    response.BaseIsSuccess = false;
                    response.BaseMessage = Shared.Constants.BAD_REQUEST;
                }

                return response;
            });
        }

        public ListChangeMoneysResponse GetResultForTimePeriod(GetResultsForTimePeriodRequest request)
        {
            return GetResponse(() => {
                var response = new ListChangeMoneysResponse();
                try
                {
                    using (FamilyFinanceContext db = new FamilyFinanceContext())
                    {
                        if (request.Start == null && request.End == null)
                        {
                            response.BaseMessage = Shared.Constants.BAD_REQUEST;
                            response.IsSuccess = false;
                        }
                        else
                        {
                            if (request.PersonId != null || request.FamilyId != null)
                            {
                                response.ChangeMoneys = db.ChangeMoneys.Where(x => (x.PersonId == request.PersonId || x.FamilyId ==request.FamilyId) && (x.Date >= request.Start && x.Date <= request.End)).ToList();
                            }
                            else
                            {
                                response.ChangeMoneys = request.FamilyId == null ?
                                db.ChangeMoneys.Where(x => x.PersonId == request.PersonId && (x.Date >= request.Start && x.Date <= request.End)).ToList():
                                db.ChangeMoneys.Where(x => x.FamilyId == request.FamilyId && (x.Date >= request.Start && x.Date <= request.End)).ToList();
                            }

                            if (response.ChangeMoneys == default)
                            {
                                response.BaseMessage = Shared.Constants.NOT_FOUND;
                                response.IsSuccess = false;
                            }
                        }
                    }
                }
                catch
                {
                    response.BaseIsSuccess = false;
                    response.BaseMessage = Shared.Constants.BAD_REQUEST;
                }

                return response;
            });
        }

        public BaseResponse AddIncomeOrExpense(ChangeMoneyRequest request)
        {
            return GetResponse(() => {
                var response = new BaseResponse();
                try
                {
                    using (FamilyFinanceContext db = new FamilyFinanceContext())
                    {
                        if(request.FamilyId == null && request.PersonId == null)
                        {
                            response.BaseIsSuccess = false;
                            response.BaseMessage = Shared.Constants.NEED_AUTHORIZE;
                        }
                        else
                        {
                            var category = db.Categories.Where(x => x.Name == request.Category.Name && x.FamilyId == request.FamilyId).FirstOrDefault();
                            var currency = db.Currencies.Where(x => x.Name == request.Currency.Name).FirstOrDefault();
                            var purse = db.Purses.Where(x => x.Name == request.Purse.Name && x.FamilyId == request.FamilyId).FirstOrDefault();
                            if (request.Type == "I")
                            {
                                purse.Size += request.Size;
                            }
                            else
                            {
                                if(request.Size < purse.Size)
                                {
                                    purse.Size -= request.Size;
                                }
                                else
                                {
                                    response.BaseIsSuccess = false;
                                    response.BaseMessage = "Недостатньо коштів для операції";
                                }
                            }
                            db.Purses.Update(purse);

                            var model = new ChangeMoney
                            {
                                Name = request.Name,
                                Size = request.Size,
                                Date = request.Date,
                                Type = request.Type,
                                Purse = purse,
                                Category = category,
                                Currency = currency,
                                FamilyId = request.FamilyId,
                                PersonId = request.PersonId
                            };

                            db.ChangeMoneys.Add(model);
                            db.SaveChanges();
                        }
                    }
                }
                catch(Exception ex)
                {
                    response.BaseIsSuccess = false;
                    response.BaseMessage = ex.Message + "Inner: " + ex.InnerException.Message;
                }

                return response;
            });
        }
    }
}
