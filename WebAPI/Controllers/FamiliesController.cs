using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Requests.FamiliesRequests;
using Shared.Models.Responses;
using WebAPI.Models;
using WebAPI.Models.APIModels;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("families")]
    public class FamiliesController : ControllerBase
    {
        private readonly FamilyFinanceContext db;
        public FamiliesController(FamilyFinanceContext context)
        {
            db = context;
        }

        [Route("add_new_family")]
        public async Task<BaseResponse> AddNewFamilyAsync([FromBody]FamilyRequest request)
        {
            var response = new BaseResponse();
            try
            {
                if(request.Family == null)
                {
                    response.IsError = true;
                    response.Message = "Cannot add NULL";
                    return response;
                }
                if (db.Families.Contains(request.Family))
                {
                    response.IsError = true;
                    response.Message = "Family is already exist";
                    return response;
                }

                db.Families.Add(request.Family);
                await db.SaveChangesAsync();
                return response;
            }
            catch
            {
                response.IsError = true;
                response.Message = "Bad request";
                return response;
            }
        }
    }
}
