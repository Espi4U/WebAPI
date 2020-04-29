using Microsoft.EntityFrameworkCore;
using Shared.Models.Requests.PursesRequests;
using Shared.Models.Responses;
using Shared.Models.Responses.PursesResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Models.APIModels;
using WebAPI.Models.APIModels.Requests;

namespace WebAPI.Services
{
    public class PurseService : BaseService
    {
        public BaseResponse AddPurse(PurseRequest request)
        {
            return GetResponse(() => {
                var response = new BaseResponse();
                try
                {
                    using (FamilyFinanceContext db = new FamilyFinanceContext())
                    {
                        if (request.Purse == null)
                        {
                            response.BaseIsSuccess = false;
                            response.BaseMessage = "Не можна додати порожній гаманець";
                        }
                        else if (db.Purses.Any(x => x.Name == request.Purse.Name))
                        {
                            response.BaseIsSuccess = false;
                            response.BaseMessage = "Гаманець з такою назвою вже існує";
                        }
                        else
                        {
                            var currency = db.Currencies.Where(x => x.Name == request.Purse.Currency.Name).FirstOrDefault();
                            var model = new Purse
                            {
                                Name = request.Purse.Name,
                                Size = request.Purse.Size,
                                Currency = currency,
                                FamilyId = request.Purse.FamilyId,
                                PersonId = request.Purse.PersonId
                            };

                            db.Purses.Add(model);
                            db.SaveChanges();
                        }
                    }
                }
                catch(Exception ex)
                {
                    response.BaseIsSuccess = false;
                    response.BaseMessage = ex.Message;
                }

                return response;
            });
        }

        public ListPursesResponse GetPurses(BaseRequest request)
        {
            return GetResponse(() => {
                var response = new ListPursesResponse();
                try
                {
                    using (FamilyFinanceContext db = new FamilyFinanceContext())
                    {
                        if (request.PersonId == null && request.FamilyId == null)
                        {
                            response.BaseIsSuccess = false;
                            response.BaseMessage = Shared.Constants.NEED_AUTHORIZE;
                        }
                        else
                        {
                            if(request.PersonId != null || request.FamilyId != null)
                            {
                                response.Purses = db.Purses.Where(x => x.FamilyId == request.FamilyId || x.PersonId == request.PersonId).ToList();
                            }
                            else
                            {
                                response.Purses = request.PersonId == null ?
                                db.Purses.Where(x => x.FamilyId == request.FamilyId).ToList():
                                db.Purses.Where(x => x.PersonId == request.PersonId).ToList();
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

        public BaseResponse DeletePurse(PurseRequest request)
        {
            return GetResponse(() => {
                var response = new BaseResponse();
                try
                {
                    using (FamilyFinanceContext db = new FamilyFinanceContext())
                    {
                        var purse = db.Purses.Where(x => x.Id == request.Purse.Id).FirstOrDefault();

                        db.Purses.Remove(purse);
                        db.SaveChanges();
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
