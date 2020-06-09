using Microsoft.VisualBasic;
using Shared;
using Shared.Models.Requests.CategoriesRequests;
using Shared.Models.Responses;
using Shared.Models.Responses.CategoriesResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Models.APIModels;
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
                            response.BaseMessage = "Не можна додати порожню категорію";
                        }
                        else if (db.Categories.Any(x=>x.Name == request.Category.Name && x.FamilyId == request.Category.FamilyId))
                        {
                            response.BaseIsSuccess = false;
                            response.BaseMessage = "Така категорія вже існує";
                        }
                        else
                        {
                            var family = db.Families.Where(x => x.Id == request.Category.FamilyId).FirstOrDefault();

                            var model = new Category
                            {
                                Name = request.Category.Name,
                                Family = family
                            };
                            db.Categories.Add(model);
                            db.SaveChanges();
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

        public ListCategoriesResponse GetCategories(BaseRequest request)
        {
            return GetResponse(() => {
                var response = new ListCategoriesResponse();
                try
                {
                    using (FamilyFinanceContext db = new FamilyFinanceContext())
                    {
                        response.Categories = db.Categories.Where(x => x.FamilyId == request.FamilyId).ToList();
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
                    response.BaseMessage = Shared.Constants.BAD_REQUEST;
                }

                return response;
            });
        }
    }
}
