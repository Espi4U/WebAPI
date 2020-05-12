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

        public LoginPageView()
        {
            _apiClient = new APIClient();

            Model = new LoginRequest();

            LoginCommand = new Command(LoginAsync);

            BindingContext = this;
            InitializeComponent();
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

            Navigation.PushAsync(new MainPageView());
        }
    }
}