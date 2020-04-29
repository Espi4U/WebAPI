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
                        if(request.FamilyId == 0 && request.PersonId == 0)
                        {
                            response.BaseMessage = Shared.Constants.NEED_AUTHORIZE;
                            response.IsSuccess = false;
                        }
                        else
                        {
                            response.ChangeMoneys = request.FamilyId == 0 ? db.ChangeMoneys.Where(x => x.PersonId == request.PersonId && x.Type == request.Type).ToList() :
                                    db.ChangeMoneys.Where(x => x.FamilyId == request.FamilyId && x.Type == request.Type).ToList();
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
                        if (request.FamilyId == 0 && request.PersonId == 0)
                        {
                            response.BaseMessage = Shared.Constants.NEED_AUTHORIZE;
                            response.IsSuccess = false;
                        }
                        else
                        {
                            response.ChangeMoney = request.FamilyId == 0 ? db.ChangeMoneys.Where(x => x.PersonId == request.PersonId && x.Type == request.Type).Max() :
                                    db.ChangeMoneys.Where(x => x.FamilyId == request.FamilyId && x.Type == request.Type).Max();

                            if (response.ChangeMoney == null)
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

        public ChangeMoneyResponse GetSmallestIncomeOrExpense(GetIncomesOrExpensesRequest request)
        {
            return GetResponse(() => {
                var response = new ChangeMoneyResponse();
                try
                {
                    using (FamilyFinanceContext db = new FamilyFinanceContext())
                    {
                        if (request.FamilyId == 0 && request.PersonId == 0)
                        {
                            response.BaseMessage = Shared.Constants.BAD_REQUEST;
                            response.IsSuccess = false;
                        }
                        else
                        {
                            response.ChangeMoney = request.FamilyId == 0 ? db.ChangeMoneys.Where(x => x.PersonId == request.PersonId && x.Type == request.Type).Min() :
                                    db.ChangeMoneys.Where(x => x.FamilyId == request.FamilyId && x.Type == request.Type).Min();

                            if (response.ChangeMoney == default)
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
                            response.ChangeMoneys = request.FamilyId == 0 ? db.ChangeMoneys.Where(x => x.PersonId == request.PersonId && (x.Date >= request.Start && x.Date <= request.End)).ToList() :
                                    db.ChangeMoneys.Where(x => x.FamilyId == request.FamilyId && (x.Date >= request.Start && x.Date <= request.End)).ToList();

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
                        if(request.ChangeMoney.FamilyId == 0 && request.ChangeMoney.PersonId == 0)
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

        public ListChangeMoneysResponse GetAdviceForTimePeriod(BaseRequest request/*need change*/)
        {
            return GetResponse(() => {
                var response = new ListChangeMoneysResponse();
                try
                {
                    using (FamilyFinanceContext db = new FamilyFinanceContext())
                    {
                        // code here
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
