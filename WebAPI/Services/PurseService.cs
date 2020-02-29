﻿using Shared.Models.Requests.PursesRequests;
using Shared.Models.Responses;
using Shared.Models.Responses.PursesResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
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
                        if (request.Purse == default)
                        {
                            response.BaseIsSuccess = false;
                            response.BaseMessage = "Cannot add empty purse";
                        }
                        else if (db.Purses.Contains(request.Purse))
                        {
                            response.BaseIsSuccess = false;
                            response.BaseMessage = "Purse is already exist";
                        }
                        else
                        {
                            db.Purses.Add(request.Purse);
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

        public ListPursesResponse GetPurses(IdRequest request)
        {
            return GetResponse(() => {
                var response = new ListPursesResponse();
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
                            response.Purses = request.PersonId == default ? db.Purses.Where(x => x.FamilyId == request.FamilyId).ToList() :
                            db.Purses.Where(x => x.PersonId == request.PersonId).ToList();
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
