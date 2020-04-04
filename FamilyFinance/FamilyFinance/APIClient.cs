using Newtonsoft.Json;
using Shared.Models.Requests.CategoriesRequests;
using Shared.Models.Requests.FamiliesRequests;
using Shared.Models.Responses;
using Shared.Models.Responses.CategoriesResponses;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Models.APIModels.Requests;
using WebAPI.Models.APIModels.Requests.ReportsControllerRequests;
using WebAPI.Models.APIModels.Responses;

namespace FamilyFinance
{
    public class APIClient
    {
        static HttpClient httpClient;

        public APIClient()
        {
            httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:44373/api/v1/")
            };
        }

        #region REPORTS

        public async Task<ListReportsResponse> GetReportsAsync(BaseRequest request) =>
            await TryCallApiAsync<ListReportsResponse>("reports/get_reports", request);

        public async Task<BaseResponse> DeleteReportAsync(ReportRequest request) =>
            await TryCallApiAsync<BaseResponse>("reports/delete_report", request);

        #endregion

        #region FAMILY

        public async Task<BaseResponse> AddFamilyAsync(FamilyRequest request) =>
            await TryCallApiAsync<BaseResponse>("families/add_family", request);

        #endregion

        private async Task<T> TryCallApiAsync<T>(string apiUrl, object request = null) where T : ApiResponse, new()
        {
            try
            {
                var uri = $"{httpClient.BaseAddress}{apiUrl}";
                var content = new StringContent(JsonConvert.SerializeObject(request ?? new object()), Encoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = null;

                responseMessage = await httpClient.PostAsync(uri, content);

                var responseContext = await responseMessage.Content.ReadAsStringAsync();

                var response = JsonConvert.DeserializeObject<T>(responseContext);
                return response;
            }
            catch(Exception ex)
            {
                return new T
                {
                    ApiMessage = $"Client side ERROR: {ex.Message}",
                    IsSuccess = false
                };
            }
        }
    }
}
