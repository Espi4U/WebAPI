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

        public ListPersonsResponse GetPersons(BaseRequest request)
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
                }
                catch
                {
                    response.BaseIsSuccess = false;
                    response.BaseMessage = "Bad request";
                }

                return response;
            });
        }

        public PersonResponse GetPersonByUserData(string login, string passwordHash, int pinCode)
        {
            return GetResponse(() => {
                var response = new PersonResponse();
                try
                {
                    using (FamilyFinanceContext db = new FamilyFinanceContext())
                    {
                        if (login == default || passwordHash == default || pinCode == default)
                        {
                            response.IsSuccess = false;
                            response.BaseMessage = "Auth data is empty";
                        }
                        else
                        {
                            response.Person = db.Persons.Where(x => x.Login == login && x.PasswordHash == passwordHash && x.PINCode == pinCode).FirstOrDefault();
                            if(response.Person == default)
                            {
                                response.IsSuccess = false;
                                response.BaseMessage = "Not find anyone persons";
                            }
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
