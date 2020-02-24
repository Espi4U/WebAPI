using Shared.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Services
{
    public abstract class BaseService
    {
        protected async Task<T> GetResponseAsync<T>(Func<Task<T>> func) where T : ApiResponse, new()
        {
            T response;

            try
            {
                response = await func();
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
