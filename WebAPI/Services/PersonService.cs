using Shared;
using Shared.Models.Requests.BaseRequests;
using Shared.Models.Responses;
using Shared.Models.Responses.PersonsResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Models.APIModels;
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
                        if (request.Person == null)
                        {
                            response.BaseIsSuccess = false;
                            response.BaseMessage = "Не можна додати порожню особу";
                        }
                        else if (db.Persons.Any(x=> x.Name == request.Person.Name && x.FamilyId == request.Person.FamilyId))
                        {
                            response.BaseIsSuccess = false;
                            response.BaseMessage = "Така особа вже є";
                        }
                        else
                        {
                            var family = db.Families.Where(x => x.Id == request.Person.FamilyId).FirstOrDefault();

                            var model = new Person
                            {
                                Name = request.Person.Name,
                                Role = request.Person.Role,
                                Login = request.Person.Login,
                                Password = request.Person.Password,
                                Family = family
                            };

                            db.Persons.Add(model);
                            db.SaveChanges();
                        }
                    }
                }
                catch
                {
                    response.BaseIsSuccess = false;
                    response.BaseMessage = Constants.BAD_REQUEST;
                }

                return response;
            });
        }

        public LoginResponse Login(LoginRequest request)
        {
            return GetResponse(() => {
                var response = new LoginResponse();
                try
                {
                    using (FamilyFinanceContext db = new FamilyFinanceContext())
                    {
                        if (request.Login == null || request.Password == null)
                        {
                            response.BaseIsSuccess = false;
                            response.BaseMessage = "Помилка, пусті дані";
                        }
                        else
                        {
                            var currentPerson = db.Persons.Where(x => x.Login == request.Login && x.Password == request.Password).FirstOrDefault();

                            var heresFamily = db.Families.Where(x => x.Id == currentPerson.FamilyId).FirstOrDefault();

                            if(currentPerson == default)
                            {
                                response.BaseIsSuccess = false;
                                response.BaseMessage = "Помилка, нікого не знайдено";
                            }
                            else
                            {
                                response.PersonName = currentPerson.Name;
                                response.Role = currentPerson.Role;
                                response.FamilyId = currentPerson.FamilyId;
                                response.PersonId = currentPerson.Id;
                                response.FamilyName = heresFamily.Name;
                            }
                        }
                    }
                }
                catch
                {
                    response.BaseIsSuccess = false;
                    response.BaseMessage = Constants.BAD_REQUEST;
                }

                return response;
            });
        }
    }
}
