using Shared.Models.Requests.BaseRequests;
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
                        if(request.ChangeMoney.FamilyId == null && request.ChangeMoney.PersonId == null)
                        {
                            response.BaseIsSuccess = false;
                            response.BaseMessage = Shared.Constants.NEED_AUTHORIZE;
                        }
                        else
                        {
                            var category = db.Categories.Where(x => x.Name == request.ChangeMoney.Category.Name).FirstOrDefault();
                            var currency = db.Currencies.Where(x => x.Name == request.ChangeMoney.Currency.Name).FirstOrDefault();

                            var model = new ChangeMoney
                            {
                                Name = request.ChangeMoney.Name,
                                Size = request.ChangeMoney.Size,
                                Date = request.ChangeMoney.Date,
                                Type = request.ChangeMoney.Type,
                                Category = category,
                                Currency = currency,
                                FamilyId = request.ChangeMoney.FamilyId,
                                PersonId = request.ChangeMoney.PersonId
                            };

                            db.ChangeMoneys.Add(model);
                            db.SaveChanges();
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
    }
}
