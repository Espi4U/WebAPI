using Shared.Models.Requests;
using Shared.Models.Requests.PurposesRequests;
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
                        if (db.Purposes.Any(x => x.Name == request.Name && x.FamilyId == request.FamilyId))
                        {
                            response.BaseIsSuccess = false;
                            response.BaseMessage = "Така ціль заощадження вже є";
                        }
                        else
                        {
                            var currency = db.Currencies.Where(x => x.Id == request.CurrencyId).FirstOrDefault();

                            var model = new Purpose
                            {
                                Name = request.Name,
                                FinalSize = request.FinalSize,
                                CurrentSize = 0,
                                Currency = currency,
                                FamilyId = request.FamilyId,
                                PersonId = request.PersonId
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
                            response.Purposes = db.Purposes.Where(x => (x.FamilyId == request.FamilyId && x.PersonId == request.PersonId) ||
                                (x.FamilyId == request.FamilyId && x.PersonId == null)).ToList();
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

        public BaseResponse UpdatePurpose(UpdatePurposeRequest request)
        {
            return GetResponse(() => {
                var response = new BaseResponse();
                try
                {
                    using (FamilyFinanceContext db = new FamilyFinanceContext())
                    {
                        var purpose = db.Purposes.Where(x => x.Id == request.PurposeId).FirstOrDefault();
                        var purse = db.Purses.Where(x => x.Id == request.PurseId).FirstOrDefault();
                        if(purse.Size <= request.NewSize)
                        {
                            response.BaseIsSuccess = false;
                            response.BaseMessage = "Недостатньо коштів";
                        }
                        if(purpose.CurrentSize + request.NewSize > purpose.FinalSize)
                        {
                            response.BaseIsSuccess = false;
                            response.BaseMessage = "Перевищення ліміту";
                        }
                        if(purpose != null && purse != null)
                        {
                            purse.Size -= request.NewSize;
                            purpose.CurrentSize += request.NewSize;

                            db.Purses.Update(purse);
                            db.Purposes.Update(purpose);
                            db.SaveChanges();
                        }
                        else
                        {
                            response.BaseIsSuccess = false;
                            response.BaseMessage = "Помилка додавання";
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
