using Microsoft.AspNetCore.Mvc;
using Shared.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.APIModels.Requests;

namespace WebAPI.Services
{
    public class AccountService : BaseService
    {
        public void Registration()
        {

        }

        public LoginResponse Login([FromBody]BaseRequest request)
        {
            return GetResponse(()=> 
            {
                var response = new LoginResponse();
                try
                {

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
