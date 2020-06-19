using Acr.UserDialogs;
using Shared.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FamilyFinance.Helpers
{
    public static class AlertHelper
    {
        public static void ShowAlertMessage<T>(T response, Page page) where T : BaseResponse
        {
            if (!string.IsNullOrWhiteSpace(response.ApiMessage))
            {
                var aConfi = new AlertConfig();
                aConfi.SetMessage(response.ApiMessage);
                aConfi.SetTitle("Помилка");
                aConfi.SetOkText("Ок");
                UserDialogs.Instance.Alert(aConfi);
            }
            if (!string.IsNullOrWhiteSpace(response.BaseMessage))
            {
                var aConfi = new AlertConfig();
                aConfi.SetMessage(response.BaseMessage);
                aConfi.SetTitle("Помилка");
                aConfi.SetOkText("Ок");
                UserDialogs.Instance.Alert(aConfi);
            }
        }
    }
}
