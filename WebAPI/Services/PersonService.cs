using Shared.Models.Responses;
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

        public ListPersonsResponse GetPersons(IdRequest request)
        {
            return GetResponse(() => {
                var response = new ListPersonsResponse();
                try
                {
                    using (FamilyFinanceContext db = new FamilyFinanceContext())
                    {
                        if (request.FamilyId == default)
                        {
                            response.BaseIsSuccess = false;
                            response.BaseMessage = "Cannot add empty person";
                        }
                        else
                        {
                            response.Persons = db.Persons.Where(x => x.FamilyId == request.FamilyId).ToList();
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
    }
}
