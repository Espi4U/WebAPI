using Shared.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FamilyFinance.Helpers
{
    public static class AlertHelper
    {
        public async static void ShowAlertMessage<T>(T response, Page page) where T : BaseResponse
        {
            if (!string.IsNullOrWhiteSpace(response.ApiMessage))
            {
                await page.DisplayAlert("Client Error", response.ApiMessage, "Ok");
            }
            if (!string.IsNullOrWhiteSpace(response.BaseMessage))
            {
                await page.DisplayAlert("Client Error", response.BaseMessage, "Ok");
            }
        }
    }
}
