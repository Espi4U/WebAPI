using Shared.Models.Requests.FamiliesRequests;
using Shared.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class FamilyService : BaseService
    {
        public BaseResponse AddFamily(FamilyRequest request)
        {
            return GetResponse(() => {
                var response = new BaseResponse();
                try
                {
                    using (FamilyFinanceContext db = new FamilyFinanceContext())
                    {
                        if (request.Family == default)
                        {
                            response.BaseIsSuccess = false;
                            response.BaseMessage = "Cannot add empty family";
                        }
                        else if (db.Families.Contains(request.Family))
                        {
                            response.BaseIsSuccess = false;
                            response.BaseMessage = "Family is already exist";
                        }
                        else
                        {
                            db.Families.Add(request.Family);
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
    }
}
