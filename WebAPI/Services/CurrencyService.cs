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
                    response.BaseMessage = Shared.Constants.BAD_REQUEST;
                }

                return response;
            });
        }
    }
}
