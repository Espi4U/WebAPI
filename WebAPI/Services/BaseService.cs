using Shared.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services
{
    public abstract class BaseService
    {
        protected T GetResponse<T>(Func<T> func) where T : ApiResponse, new()
        {
            T response;
            try
            {
                response = func();
            }
            catch (Exception e)
            {
                response = new T
                {
                    IsSuccess = false,
                    ApiMessage = e.Message
                };
                if (e.InnerException != null)
                {
                    response.ApiMessage += e.InnerException.Message;
                }
            }
            return response;
        }
    }
}