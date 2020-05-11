using FamilyFinance.Helpers;
using Shared.Models.Requests.BaseRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FamilyFinance.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPageView : ContentPage
    {
        private APIClient _apiClient;

        public string Login { get; set; }
        public string Password { get; set; }

        public ICommand LoginCommand { get; }

        public LoginPageView()
        {
            _apiClient = new APIClient();

            LoginCommand = new Command(LoginAsync);

            BindingContext = this;
            InitializeComponent();
        }

        private async void LoginAsync()
        {
            var request = new LoginRequest
            {
                Login = Login,
                Password = Password
            };
            var response = await _apiClient.LoginAsync(request);
            if(!response.BaseIsSuccess || !response.IsSuccess)
            {
                AlertHelper.ShowAlertMessage(response, this);
                return;
            }

            GlobalHelper.SetPersonId(response.PersonId);
            GlobalHelper.SetFamilyId(response.FamilyId);
            GlobalHelper.SetFamilyName(response.FamilyName);
            GlobalHelper.SetPersonName(response.PersonName);
            GlobalHelper.SetRole(response.Role);
        }
    }
}