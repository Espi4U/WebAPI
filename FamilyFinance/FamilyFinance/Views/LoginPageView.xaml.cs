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

        private LoginRequest _model;
        public LoginRequest Model
        {
            get => _model;
            set
            {
                _model = value;
                OnPropertyChanged(nameof(Model));
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand RegistrationCommand { get; }

        public LoginPageView()
        {
            _apiClient = new APIClient();

            Model = new LoginRequest();

            LoginCommand = new Command(LoginAsync);
            RegistrationCommand = new Command(RegistrationAsync);

            BindingContext = this;
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void LoginAsync()
        {
            var response = await _apiClient.LoginAsync(Model);
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