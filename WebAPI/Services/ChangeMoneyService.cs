using Shared.Models.Requests.BaseRequests;
using Shared.Models.Requests.ChangeMoneyRequests;
using Shared.Models.Responses.ChangeMoneysResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
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
                        if(request.FamilyId == default && request.PersonId == default)
                        {
                            response.BaseMessage = "BadRequest";
                            response.IsSuccess = false;
                        }
                        else
                        {
                            response.ChangeMoneys = request.FamilyId == default ? db.ChangesMoney.Where(x => x.PersonId == request.PersonId && x.Type == request.Type).ToList() :
                                    db.ChangesMoney.Where(x => x.FamilyId == request.FamilyId && x.Type == request.Type).ToList();
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

        public ListChangeMoneysResponse GetDataForReport(BaseRequest request/*need change*/)
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
                    response.BaseMessage = "Bad request";
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
                        if (request.FamilyId == default && request.PersonId == default)
                        {
                            response.BaseMessage = "Bad Request";
                            response.IsSuccess = false;
                        }
                        else
                        {
                            response.ChangeMoney = request.FamilyId == default ? db.ChangesMoney.Where(x => x.PersonId == request.PersonId && x.Type == request.Type).Max() :
                                    db.ChangesMoney.Where(x => x.FamilyId == request.FamilyId && x.Type == request.Type).Max();

                            if (response.ChangeMoney == default)
                            {
                                response.BaseMessage = "Not Found";
                                response.IsSuccess = false;
                            }
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

        public ChangeMoneyResponse GetSmallestIncomeOrExpense(GetIncomesOrExpensesRequest request)
        {
            return GetResponse(() => {
                var response = new ChangeMoneyResponse();
                try
                {
                    using (FamilyFinanceContext db = new FamilyFinanceContext())
                    {
                        if (request.FamilyId == default && request.PersonId == default)
                        {
                            response.BaseMessage = "Bad Request";
                            response.IsSuccess = false;
                        }
                        else
                        {
                            response.ChangeMoney = request.FamilyId == default ? db.ChangesMoney.Where(x => x.PersonId == request.PersonId && x.Type == request.Type).Min() :
                                    db.ChangesMoney.Where(x => x.FamilyId == request.FamilyId && x.Type == request.Type).Min();

                            if (response.ChangeMoney == default)
                            {
                                response.BaseMessage = "Not Found";
                                response.IsSuccess = false;
                            }
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

        public ListChangeMoneysResponse GetResultForTimePeriod(GetResultsForTimePeriodRequest request)
        {
            return GetResponse(() => {
                var response = new ListChangeMoneysResponse();
                try
                {
                    using (FamilyFinanceContext db = new FamilyFinanceContext())
                    {
                        if (request.Start == default && request.End == default)
                        {
                            response.BaseMessage = "Bad Request";
                            response.IsSuccess = false;
                        }
                        else
                        {
                            response.ChangeMoneys = request.FamilyId == default ? db.ChangesMoney.Where(x => x.PersonId == request.PersonId && (x.Date >= request.Start && x.Date <= request.End)).ToList() :
                                    db.ChangesMoney.Where(x => x.FamilyId == request.FamilyId && (x.Date >= request.Start && x.Date <= request.End)).ToList();

                            if (response.ChangeMoneys == default)
                            {
                                response.BaseMessage = "Not Found";
                                response.IsSuccess = false;
                            }
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
                    response.BaseMessage = "Bad request";
                }

                return response;
            });
        }
    }
}
