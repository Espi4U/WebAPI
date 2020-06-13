using Acr.UserDialogs;
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

        private string _login;
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand RegistrationCommand { get; }

        public LoginPageView()
        {
            _apiClient = new APIClient();

            LoginCommand = new Command(LoginAsync);
            RegistrationCommand = new Command(RegistrationAsync);

            BindingContext = this;
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void LoginAsync()
        {
            UserDialogs.Instance.ShowLoading();
            var request = new LoginRequest
            {
                Login = Login,
                Password = Password
            };
            var response = await _apiClient.LoginAsync(request);
            if(!response.BaseIsSuccess || !response.IsSuccess)
            {
                AlertHelper.ShowAlertMessage(response, this);
                UserDialogs.Instance.HideLoading();
                return;
            }

            GlobalHelper.SetPersonId(response.PersonId);
            GlobalHelper.SetFamilyId(response.FamilyId);
            GlobalHelper.SetFamilyName(response.FamilyName);
            GlobalHelper.SetPersonName(response.PersonName);
            GlobalHelper.SetRole(response.Role);

            UserDialogs.Instance.HideLoading();

            await Navigation.PushAsync(new MainPageView());
        }

        private async void RegistrationAsync(object parameter)
        {
            if(parameter.ToString() == "NEW")
            {
                await Navigation.PushAsync(new RegistrationNewPageView());
            }
            else
            {
                await Navigation.PushAsync(new RegistrationNewWithKeyPageView());
            }
        }
    }
}