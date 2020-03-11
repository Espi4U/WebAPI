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
        public BaseResponse AddCategory(CategoryRequest request)
        {
            return GetResponse(() => {
                var response = new BaseResponse();
                try
                {
                    using (FamilyFinanceContext db = new FamilyFinanceContext())
                    {
                        if (request.Category == default)
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
                }
                catch
                {
                    response.BaseIsSuccess = false;
                    response.BaseMessage = "Bad request";
                }

                return response;
            });
        }

        public ListCategoriesResponse GetCategories(BaseRequest request)
        {
            return GetResponse(() => {
                var response = new ListCategoriesResponse();
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
                            response.Categories = request.PersonId == default ? db.Categories.Where(x => x.FamilyId == request.FamilyId).ToList() :
                            db.Categories.Where(x => x.PersonId == request.PersonId).ToList();
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

        public BaseResponse DeleteCategory(CategoryRequest request)
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
