using Shared.Models.Responses;
using Shared.Models.Responses.PersonsResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Models.APIModels.Requests;
using WebAPI.Models.APIModels.Requests.PersonsControllerRequests;
using WebAPI.Models.APIModels.Responses.PersonsControllerResponses;

namespace WebAPI.Services
{
    public class PersonService : BaseService
    {
        public BaseResponse AddPerson(PersonRequest request)
        {
            return GetResponse(() => {
                var response = new BaseResponse();
                try
                {
                    using (FamilyFinanceContext db = new FamilyFinanceContext())
                    {
                        if (request.Person == default || request.Person.FamilyId == default)
                        {
                            response.BaseIsSuccess = false;
                            response.BaseMessage = "Cannot add empty person";
                        }
                        else if (db.Persons.Contains(request.Person))
                        {
                            response.BaseIsSuccess = false;
                            response.BaseMessage = "Person is already exist";
                        }
                        else
                        {
                            db.Persons.Add(request.Person);
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
