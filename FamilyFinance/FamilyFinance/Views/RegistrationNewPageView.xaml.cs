using FamilyFinance.Helpers;
using Shared.Models.Requests;
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
    public partial class RegistrationNewPageView : ContentPage
    {
        private APIClient _apiClient;

        private string _familyName;
        public string FamilyName
        {
            get => _familyName;
            set
            {
                _familyName = value;
                OnPropertyChanged(nameof(FamilyName));
            }
        }

        private string _personName;
        public string PersonName
        {
            get => _personName;
            set
            {
                _personName = value;
                OnPropertyChanged(nameof(PersonName));
            }
        }

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

        public ICommand RegistrationCommand { get; }

        public RegistrationNewPageView()
        {
            _apiClient = new APIClient();

            RegistrationCommand = new Command(RegistrationAsync);

            BindingContext = this;
            InitializeComponent();
        }

        private async void RegistrationAsync()
        {
            var request = new RegistrationRequest
            {
                FamilyName = FamilyName,
                PersonName = PersonName,
                Login = Login,
                Password = Password,
            };
            var response = await _apiClient.RegistrationNewAsync(request);
            if(!response.BaseIsSuccess || !response.IsSuccess)
            {
                AlertHelper.ShowAlertMessage(response, this);
                return;
            }

            await Navigation.PushAsync(new LoginPageView());
        }
    }
}