using Acr.UserDialogs;
using FamilyFinance.Helpers;
using FamilyFinance.Models;
using Shared.Models.Requests.BaseRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        private Field _login;
        public Field Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        private Field _password;
        public Field Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public ICommand RegistrationCommand { get; }
        public ICommand OnValidationCommand { get; set; }

        public LoginPageView()
        {
            _apiClient = new APIClient();

            Login = new Field();
            Password = new Field();

            RegistrationCommand = new Command(RegistrationAsync);
            OnValidationCommand = new Command(Validation);

            BindingContext = this;
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void LoginAsync()
        {
            var request = new LoginRequest
            {
                Login = Login.Name,
                Password = Password.Name
            };
            var response = await _apiClient.LoginAsync(request);
            if(!response.BaseIsSuccess || !response.IsSuccess)
            {
                return;
            }

            GlobalHelper.SetPersonId(response.PersonId);
            GlobalHelper.SetFamilyId(response.FamilyId);
            GlobalHelper.SetFamilyName(response.FamilyName);
            GlobalHelper.SetPersonName(response.PersonName);
            GlobalHelper.SetRole(response.Role);
            GlobalHelper.SetToken(response.Token);

            App.Current.MainPage = new NavigationPage(new MainPageView());
        }

        private void Validation()
        {
            if (string.IsNullOrEmpty(Login.Name))
            {
                Login.NotValidMessageError = "Обов'язкове поле";
                Login.IsNotValid = true;
            }
            else if(!Regex.IsMatch(Login.Name, Constants.NameLatinPattern))
            {
                Login.NotValidMessageError = "Тільки латинські символи. Мінімум 4, максимум 10";
                Login.IsNotValid = true;
            }
            else
            {
                Login.IsNotValid = false;
            }

            if (string.IsNullOrEmpty(Password.Name))
            {
                Password.NotValidMessageError = "Обов'язкове поле";
                Password.IsNotValid = true;
            }
            else if(!Regex.IsMatch(Password.Name, Constants.NameLatinPattern))
            {
                Password.NotValidMessageError = "Тільки латинські символи. Мінімум 4, максимум 10";
                Password.IsNotValid = true;
            }
            else
            {
                Password.IsNotValid = false;
            }

            if(!Login.IsNotValid && !Password.IsNotValid)
            {
                LoginAsync();
            }
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