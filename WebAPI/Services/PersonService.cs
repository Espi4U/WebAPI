using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Models.Requests;
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

        public BaseResponse RegistrationNew(RegistrationRequest request)
        {
            return GetResponse(() => {
                var response = new BaseResponse();
                try
                {
                    using (FamilyFinanceContext db = new FamilyFinanceContext())
                    {
                        if (request == null)
                        {
                            response.BaseIsSuccess = false;
                            response.BaseMessage = "Помилка, пусті дані";
                        }
                        else
                        {
                            var family = new Family
                            {
                                Name = request.FamilyName
                            };

                            db.Families.Add(family);

                            var person = new Person
                            {
                                Name = request.PersonName,
                                Role = "H",
                                Family = family,
                                Login = request.Login,
                                Password = request.Password
                            };

                            db.Persons.Add(person);
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

        public BaseResponse RegistrationNewWithKey(RegistrationRequest request)
        {
            return GetResponse(() => {
                var response = new BaseResponse();
                try
                {
                    using (FamilyFinanceContext db = new FamilyFinanceContext())
                    {
                        if (request == null)
                        {
                            response.BaseIsSuccess = false;
                            response.BaseMessage = "Помилка, пусті дані";
                        }
                        else if (request.Key != null && request.FamilyId != null)
                        {
                            if(!db.InviteKeys.Any(x => x.FamilyId == request.FamilyId && x.Key == request.Key))
                            {
                                response.BaseIsSuccess = false;
                                response.BaseMessage = "Помилка, Невірний ключ";
                            }
                            else
                            {
                                var key = db.InviteKeys.Where(x => x.Key == request.Key).FirstOrDefault();
                                db.InviteKeys.Remove(key);

                                var family = db.Families.Where(x => x.Id == request.FamilyId).FirstOrDefault();

                                var person = new Person
                                {
                                    Name = request.PersonName,
                                    Role = "U",
                                    Family = family,
                                    Login = request.Login,
                                    Password = request.Password
                                };

                                db.Persons.Add(person);
                                db.SaveChanges();

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
