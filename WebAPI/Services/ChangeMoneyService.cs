using Shared.Models.Requests;
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
                            response.ChangeMoneys = db.ChangeMoneys.Where(x => x.Type == request.Type && x.FamilyId == request.FamilyId && x.PersonId == request.PersonId).ToList();
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

        public ListChangeMoneysResponse GetAllIncomesOrExpenses(BaseRequest request)
        {
            return GetResponse(() => {
                var response = new ListChangeMoneysResponse();
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
                            response.ChangeMoneys = db.ChangeMoneys.Where(x => x.FamilyId == request.FamilyId && x.PersonId == request.PersonId).ToList();
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
                            response.ChangeMoneys = db.ChangeMoneys.Where(x => (x.PersonId == request.PersonId && x.FamilyId == request.FamilyId) && (x.Date >= request.Start && x.Date <= request.End) && x.Type == request.Type).ToList();
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

        public ListChangeMoneysResponse GetResultForCurrency(GetResultForCurrencyRequest request)
        {
            return GetResponse(() => {
                var response = new ListChangeMoneysResponse();
                try
                {
                    using (FamilyFinanceContext db = new FamilyFinanceContext())
                    {
                        response.ChangeMoneys = db.ChangeMoneys.Where(x => x.PersonId == request.PersonId && x.FamilyId == request.FamilyId && request.CurrencyId == x.CurrencyId && request.Type == x.Type && x.Date <= request.End && x.Date >= request.Start).ToList();
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
                                if(request.Size <= purse.Size)
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
                    response.BaseMessage = ex.Message + "; Inner: " + ex.InnerException.Message;
                }

                return response;
            });
        }

        public DataByCategotyToChartResponse GetDataByCategoryToChart(BaseRequest request)
        {
            return GetResponse(() => {
                var response = new DataByCategotyToChartResponse();
                try
                {
                    using (FamilyFinanceContext db = new FamilyFinanceContext())
                    {
                        var incomes = new Dictionary<string, int>();
                        var expenses = new Dictionary<string, int>();
                        var changeMoneysIncome = db.ChangeMoneys.Where(x => x.FamilyId == request.FamilyId && x.PersonId == request.PersonId && x.Type == "I" && x.CurrencyId == 1).ToList();
                        var changeMoneysExpense = db.ChangeMoneys.Where(x => x.FamilyId == request.FamilyId && x.PersonId == request.PersonId && x.Type == "E" && x.CurrencyId == 1).ToList();
                        foreach(var changeMoney in changeMoneysIncome)
                        {
                            var category = db.Categories.Where(x => x.FamilyId == request.FamilyId && x.Id == changeMoney.CategoryId).FirstOrDefault();
                            if (incomes.ContainsKey(category.Name))
                            {
                                continue;
                            }
                            else
                            {
                                var sum = changeMoneysIncome.Where(x => x.CategoryId == category.Id).Sum(x => x.Size);
                                incomes.Add(category.Name, sum);
                            }
                        }
                        foreach (var changeMoney in changeMoneysExpense)
                        {
                            var category = db.Categories.Where(x => x.FamilyId == request.FamilyId && x.Id == changeMoney.CategoryId).FirstOrDefault();
                            if (expenses.ContainsKey(category.Name))
                            {
                                continue;
                            }
                            else
                            {
                                var sum = changeMoneysExpense.Where(x => x.CategoryId == category.Id).Sum(x => x.Size);
                                expenses.Add(category.Name, sum);
                            }
                        }

                        response.IncomesUAH = incomes;
                        response.ExpensesUAH = expenses;

                        incomes = new Dictionary<string, int>();
                        expenses = new Dictionary<string, int>();
                        changeMoneysIncome = db.ChangeMoneys.Where(x => x.FamilyId == request.FamilyId && x.PersonId == request.PersonId && x.Type == "I" && x.CurrencyId == 2).ToList();
                        changeMoneysExpense = db.ChangeMoneys.Where(x => x.FamilyId == request.FamilyId && x.PersonId == request.PersonId && x.Type == "E" && x.CurrencyId == 2).ToList();
                        foreach (var changeMoney in changeMoneysIncome)
                        {
                            var category = db.Categories.Where(x => x.FamilyId == request.FamilyId && x.Id == changeMoney.CategoryId).FirstOrDefault();
                            if (incomes.ContainsKey(category.Name))
                            {
                                continue;
                            }
                            else
                            {
                                var sum = changeMoneysIncome.Where(x => x.CategoryId == category.Id).Sum(x => x.Size);
                                incomes.Add(category.Name, sum);
                            }
                        }
                        foreach (var changeMoney in changeMoneysExpense)
                        {
                            var category = db.Categories.Where(x => x.FamilyId == request.FamilyId && x.Id == changeMoney.CategoryId).FirstOrDefault();
                            if (expenses.ContainsKey(category.Name))
                            {
                                continue;
                            }
                            else
                            {
                                var sum = changeMoneysExpense.Where(x => x.CategoryId == category.Id).Sum(x => x.Size);
                                expenses.Add(category.Name, sum);
                            }
                        }

                        response.IncomesUSD = incomes;
                        response.ExpensesUSD = expenses;

                        incomes = new Dictionary<string, int>();
                        expenses = new Dictionary<string, int>();
                        changeMoneysIncome = db.ChangeMoneys.Where(x => x.FamilyId == request.FamilyId && x.PersonId == request.PersonId && x.Type == "I" && x.CurrencyId == 3).ToList();
                        changeMoneysExpense = db.ChangeMoneys.Where(x => x.FamilyId == request.FamilyId && x.PersonId == request.PersonId && x.Type == "E" && x.CurrencyId == 3).ToList();
                        foreach (var changeMoney in changeMoneysIncome)
                        {
                            var category = db.Categories.Where(x => x.FamilyId == request.FamilyId && x.Id == changeMoney.CategoryId).FirstOrDefault();
                            if (incomes.ContainsKey(category.Name))
                            {
                                continue;
                            }
                            else
                            {
                                var sum = changeMoneysIncome.Where(x => x.CategoryId == category.Id).Sum(x => x.Size);
                                incomes.Add(category.Name, sum);
                            }
                        }
                        foreach (var changeMoney in changeMoneysExpense)
                        {
                            var category = db.Categories.Where(x => x.FamilyId == request.FamilyId && x.Id == changeMoney.CategoryId).FirstOrDefault();
                            if (expenses.ContainsKey(category.Name))
                            {
                                continue;
                            }
                            else
                            {
                                var sum = changeMoneysExpense.Where(x => x.CategoryId == category.Id).Sum(x => x.Size);
                                expenses.Add(category.Name, sum);
                            }
                        }

                        response.IncomesEUR = incomes;
                        response.ExpensesEUR = expenses;

                        incomes = new Dictionary<string, int>();
                        expenses = new Dictionary<string, int>();
                        changeMoneysIncome = db.ChangeMoneys.Where(x => x.FamilyId == request.FamilyId && x.PersonId == request.PersonId && x.Type == "I" && x.CurrencyId == 4).ToList();
                        changeMoneysExpense = db.ChangeMoneys.Where(x => x.FamilyId == request.FamilyId && x.PersonId == request.PersonId && x.Type == "E" && x.CurrencyId == 4).ToList();
                        foreach (var changeMoney in changeMoneysIncome)
                        {
                            var category = db.Categories.Where(x => x.FamilyId == request.FamilyId && x.Id == changeMoney.CategoryId).FirstOrDefault();
                            if (incomes.ContainsKey(category.Name))
                            {
                                continue;
                            }
                            else
                            {
                                var sum = changeMoneysIncome.Where(x => x.CategoryId == category.Id).Sum(x => x.Size);
                                incomes.Add(category.Name, sum);
                            }
                        }
                        foreach (var changeMoney in changeMoneysExpense)
                        {
                            var category = db.Categories.Where(x => x.FamilyId == request.FamilyId && x.Id == changeMoney.CategoryId).FirstOrDefault();
                            if (expenses.ContainsKey(category.Name))
                            {
                                continue;
                            }
                            else
                            {
                                var sum = changeMoneysExpense.Where(x => x.CategoryId == category.Id).Sum(x => x.Size);
                                expenses.Add(category.Name, sum);
                            }
                        }

                        response.IncomesPLZ = incomes;
                        response.ExpensesPLZ = expenses;
                    }
                }
                catch (Exception ex)
                {
                    response.BaseIsSuccess = false;
                    response.BaseMessage = ex.Message + "; Inner: " + ex.InnerException.Message;
                }

                return response;
            });
        }
    }
}
