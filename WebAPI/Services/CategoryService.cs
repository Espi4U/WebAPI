using Shared.Models.Requests.CategoriesRequests;
using Shared.Models.Responses;
using Shared.Models.Responses.CategoriesResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Models.APIModels.Requests;

namespace WebAPI.Services
{
    public class CategoryService : BaseService
    {
        public BaseResponse AddNewCategory(CategoryRequest request)
        {
            return GetResponse(() => {
                var response = new BaseResponse();
                try
                {
                    using (FamilyFinanceContext db = new FamilyFinanceContext())
                    {
                        if (request.Category == null)
                        {
                            response.BaseIsSuccess = false;
                            response.BaseMessage = "Cannot add empty category";
                        }
                        else if (db.Categories.Contains(request.Category))
                        {
                            response.BaseIsSuccess = false;
                            response.BaseMessage = "Categoty is already exist";
                        }
                        else
                        {
                            db.Categories.Add(request.Category);
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

        public ListCategoriesResponse GetCategoriesById(IdRequest request)
        {
            return GetResponse(() => {
                var response = new ListCategoriesResponse();
                try
                {
                    using (FamilyFinanceContext db = new FamilyFinanceContext())
                    {
                        if (request.PersonId == null && request.FamilyId == null)
                        {
                            response.BaseIsSuccess = false;
                            response.BaseMessage = "Bad request";
                        }
                        else
                        {
                            response.Categories = request.PersonId == null ? db.Categories.Where(x => x.FamilyId == request.FamilyId).ToList() :
                            db.Categories.Where(x => x.PersonId == request.PersonId).ToList();
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

        public BaseResponse DeleteCategoryById(CategoryRequest request)
        {
            return GetResponse(() => {
                var response = new BaseResponse();
                try
                {
                    using (FamilyFinanceContext db = new FamilyFinanceContext())
                    {
                        var category = db.Categories.Where(x => x.Id == request.Category.Id).FirstOrDefault();

                        db.Categories.Remove(category);
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
