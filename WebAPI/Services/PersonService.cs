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

                            db.Categories.Add(new Category { Name = "Інше", Family = family });

                            var uah = db.Currencies.Where(x => x.Name == "Українська Гривня").FirstOrDefault();
                            db.Purses.Add(new Purse { Name = "Універсальний гривневий гаманець", Size = 0, Currency = uah, Family = family });

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
                        else if (request.Key != null)
                        {
                            if(!db.InviteKeys.Any(x => x.Key == request.Key))
                            {
                                response.BaseIsSuccess = false;
                                response.BaseMessage = "Помилка, Невірний ключ";
                            }
                            else
                            {
                                var familyId = db.InviteKeys.Where(x => x.Key == request.Key).FirstOrDefault().FamilyId;
                                var key = db.InviteKeys.Where(x => x.Key == request.Key).FirstOrDefault();
                                db.InviteKeys.Remove(key);

                                var family = db.Families.Where(x => x.Id == familyId).FirstOrDefault();

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
