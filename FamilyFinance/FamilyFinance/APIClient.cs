using FamilyFinance.Helpers;
using Newtonsoft.Json;
using Shared.Models.Requests.BaseRequests;
using Shared.Models.Requests.CategoriesRequests;
using Shared.Models.Requests.CurrenciesRequests;
using Shared.Models.Requests.FamiliesRequests;
using Shared.Models.Requests.PurposesRequests;
using Shared.Models.Requests.PursesRequests;
using Shared.Models.Responses;
using Shared.Models.Responses.CategoriesResponses;
using Shared.Models.Responses.CurrenciesResponses;
using Shared.Models.Responses.PurposesResponses;
using Shared.Models.Responses.PursesResponses;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Models.APIModels.Requests;
using WebAPI.Models.APIModels.Requests.ReportsControllerRequests;
using WebAPI.Models.APIModels.Responses;
using Xamarin.Forms;

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

        #region CATEGORIES

        public async Task<BaseResponse> AddCategoryAsync(CategoryRequest request) =>
            await TryCallApiAsync<BaseResponse>("categories/add_categoty", request);

        public async Task<ListCategoriesResponse> GetCategoriesAsync(BaseRequest request) =>
            await TryCallApiAsync<ListCategoriesResponse>("categories/get_categories", request);

        public async Task<BaseResponse> DeleteCategoryAsync(CategoryRequest request) =>
            await TryCallApiAsync<BaseResponse>("categories/delete_category", request);

        #endregion

        #region CURRENCIES

        public async Task<BaseResponse> AddCurrencyAsync(CurrencyRequest request) =>
            await TryCallApiAsync<BaseResponse>("currencies/add_currency", request);

        public async Task<ListCurrenciesResponse> GetCurrenciesAsync(BaseRequest request) =>
            await TryCallApiAsync<ListCurrenciesResponse>("currencies/get_currency", request);

        public async Task<BaseResponse> DeleteCurrencyAsync(CurrencyRequest request) =>
            await TryCallApiAsync<BaseResponse>("currencies/delete_currency", request);

        #endregion

        #region PURPOSES

        public async Task<BaseResponse> AddPurposeAsync(PurposeRequest request) =>
            await TryCallApiAsync<BaseResponse>("purposes/add_purpose", request);

        public async Task<ListPurposesResponse> GetPurposesAsync(BaseRequest request) =>
            await TryCallApiAsync<ListPurposesResponse>("purposes/get_purposes", request);

        public async Task<BaseResponse> DeletePurposeAsync(PurposeRequest request) =>
            await TryCallApiAsync<BaseResponse>("purposes/delete_purpose", request);

        #endregion

        #region PURSES

        public async Task<BaseResponse> AddPurseAsync(PurseRequest request) =>
            await TryCallApiAsync<BaseResponse>("purses/add_purse", request);

        public async Task<ListPursesResponse> GetPursesAsync(BaseRequest request) =>
            await TryCallApiAsync<ListPursesResponse>("purses/get_purses", request);

        public async Task<BaseResponse> DeletePurseAsync(PurseRequest request) =>
            await TryCallApiAsync<BaseResponse>("purses/delete_purse", request);

        #endregion

        #region CHANGEMONEYS
        #endregion


        #region ACCOUNT

        public async void LogInAsync(LoginRequest request)
        {
            var response = await TryCallApiAsync<LoginResponse>("account/login", request);
            if (!response.BaseIsSuccess)
            {
                return;
            }

            GlobalHelper.WritePersonId(response.PersonId);
            GlobalHelper.WriteToken(response.Token);
        }

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
