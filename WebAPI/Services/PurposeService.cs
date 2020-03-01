using Shared.Models.Requests.PurposesRequests;
using Shared.Models.Responses;
using Shared.Models.Responses.PurposesResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Models.APIModels.Requests;

namespace WebAPI.Services
{
    public class PurposeService : BaseService
    {
        public BaseResponse AddPurpose(PurposeRequest request)
        {
            return GetResponse(() => {
                var response = new BaseResponse();
                try
                {
                    using (FamilyFinanceContext db = new FamilyFinanceContext())
                    {
                        if (request.Purpose == default)
                        {
                            response.BaseIsSuccess = false;
                            response.BaseMessage = "Cannot add empty purse";
                        }
                        else if (db.Purposes.Contains(request.Purpose))
                        {
                            response.BaseIsSuccess = false;
                            response.BaseMessage = "Purse is already exist";
                        }
                        else
                        {
                            db.Purposes.Add(request.Purpose);
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

        public ListPurposesResponse GetPurposes(IdRequest request)
        {
            return GetResponse(() => {
                var response = new ListPurposesResponse();
                try
                {
                    using (FamilyFinanceContext db = new FamilyFinanceContext())
                    {
                        if (request.PersonId == default && request.FamilyId == default)
                        {
                            response.BaseIsSuccess = false;
                            response.BaseMessage = "Bad request";
                        }
                        else
                        {
                            response.Purposes = request.PersonId == default ? db.Purposes.Where(x => x.FamilyId == request.FamilyId).ToList() :
                            db.Purposes.Where(x => x.PersonId == request.PersonId).ToList();
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

        public BaseResponse DeletePurpose(PurposeRequest request)
        {
            return GetResponse(() => {
                var response = new BaseResponse();
                try
                {
                    using (FamilyFinanceContext db = new FamilyFinanceContext())
                    {
                        var purpose = db.Purposes.Where(x => x.Id == request.Purpose.Id).FirstOrDefault();

                        db.Purposes.Remove(purpose);
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
