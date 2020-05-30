﻿using Shared.Models.Requests.PurposesRequests;
using Shared.Models.Responses;
using Shared.Models.Responses.PurposesResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Models.APIModels;
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
                        if (request.Purpose == null)
                        {
                            response.BaseIsSuccess = false;
                            response.BaseMessage = "Не можна додати порожню ціль заощадження";
                        }
                        else if (db.Purposes.Any(x => x.Name == request.Purpose.Name))
                        {
                            response.BaseIsSuccess = false;
                            response.BaseMessage = "Така ціль заощадження вже є";
                        }
                        else
                        {
                            var currency = db.Currencies.Where(x => x.Name == request.Purpose.Currency.Name).FirstOrDefault();

                            var model = new Purpose
                            {
                                Name = request.Purpose.Name,
                                FinalSize = request.Purpose.FinalSize,
                                CurrentSize = request.Purpose.CurrentSize,
                                Currency = currency,
                                FamilyId = request.Purpose.FamilyId,
                                PersonId = request.Purpose.PersonId
                            };

                            db.Purposes.Add(model);
                            db.SaveChanges();
                        }
                    }
                }
                catch(Exception ex)
                {
                    response.BaseIsSuccess = false;
                    response.BaseMessage = ex.InnerException.Message;
                }

                return response;
            });
        }

        public ListPurposesResponse GetPurposes(BaseRequest request)
        {
            return GetResponse(() => {
                var response = new ListPurposesResponse();
                try
                {
                    using (FamilyFinanceContext db = new FamilyFinanceContext())
                    {
                        if (request.PersonId == null && request.FamilyId == null)
                        {
                            response.BaseIsSuccess = false;
                            response.BaseMessage = Shared.Constants.BAD_REQUEST;
                        }
                        else
                        {
                            if(request.PersonId != null || request.FamilyId != null)
                            {
                                response.Purposes = db.Purposes.Where(x => x.FamilyId == request.FamilyId || x.PersonId == request.PersonId).ToList();
                            }
                            else
                            {
                                response.Purposes = request.PersonId == null ?
                                db.Purposes.Where(x => x.FamilyId == request.FamilyId).ToList():
                                db.Purposes.Where(x => x.PersonId == request.PersonId).ToList();
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
                }
                catch
                {
                    response.BaseIsSuccess = false;
                    response.BaseMessage = Shared.Constants.BAD_REQUEST;
                }

                return response;
            });
        }

        public BaseResponse UpdatePurpose(PurposeRequest request)
        {
            return GetResponse(() => {
                var response = new BaseResponse();
                try
                {
                    using (FamilyFinanceContext db = new FamilyFinanceContext())
                    {
                        var purpose = db.Purposes.Where(x => x.Id == request.Purpose.Id).FirstOrDefault();

                        if(purpose != null)
                        {
                            purpose.Name = request.Purpose.Name;
                            purpose.FinalSize = request.Purpose.FinalSize;
                            purpose.CurrentSize = request.Purpose.CurrentSize;
                        }

                        db.Purposes.Update(purpose);
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
