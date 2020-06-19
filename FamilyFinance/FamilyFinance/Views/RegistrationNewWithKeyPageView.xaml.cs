using Acr.UserDialogs;
using FamilyFinance.Helpers;
using FamilyFinance.Models;
using Shared.Models.Requests;
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
    public partial class RegistrationNewWithKeyPageView : ContentPage
    {
        private APIClient _apiClient;

        private Field _key;
        public Field Key
        {
            get => _key;
            set
            {
                _key = value;
                OnPropertyChanged(nameof(Key));
            }
        }

        private Field _personName;
        public Field PersonName
        {
            get => _personName;
            set
            {
                _personName = value;
                OnPropertyChanged(nameof(PersonName));
            }
        }

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

        public ICommand OnValidationCommand { get; }

        public RegistrationNewWithKeyPageView()
        {
            _apiClient = new APIClient();

            Key = new Field();
            PersonName = new Field();
            Login = new Field();
            Password = new Field();

            OnValidationCommand = new Command(Validation);

            BindingContext = this;
            InitializeComponent();
        }

        private void Validation()
        {
            if (string.IsNullOrEmpty(Login.Name))
            {
                Login.NotValidMessageError = "Обов'язкове поле";
                Login.IsNotValid = true;
            }
            else if (!Regex.IsMatch(Login.Name, Constants.NameLatinPattern))
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
            else if (!Regex.IsMatch(Password.Name, Constants.NameLatinPattern))
            {
                Password.NotValidMessageError = "Тільки латинські символи. Мінімум 4, максимум 10";
                Password.IsNotValid = true;
            }
            else
            {
                Password.IsNotValid = false;
            }

            if (string.IsNullOrEmpty(Key.Name))
            {
                Key.NotValidMessageError = "Обов'язкове поле";
                Key.IsNotValid = true;
            }
            else
            {
                Key.IsNotValid = false;
            }

            if (string.IsNullOrEmpty(PersonName.Name))
            {
                PersonName.NotValidMessageError = "Обов'язкове поле";
                PersonName.IsNotValid = true;
            }
            else if (!Regex.IsMatch(PersonName.Name, Constants.NameLatinAndCyrylicPattern))
            {
                PersonName.NotValidMessageError = "Некоректне ім'я. Мінімум 4, максимум 10";
                PersonName.IsNotValid = true;
            }
            else
            {
                PersonName.IsNotValid = false;
            }

            if (!Key.IsNotValid &&
                !PersonName.IsNotValid &&
                !Login.IsNotValid &&
                !Password.IsNotValid)
            {
                RegistrationAsync();
            }
        }

        private async void RegistrationAsync()
        {
            UserDialogs.Instance.ShowLoading();
            var request = new RegistrationRequest
            {
                PersonName = PersonName.Name,
                Login = Login.Name,
                Password = Password.Name,
                Key = Key.Name,
            };
            var response = await _apiClient.RegistrationNewWithKeyAsync(request);
            if (!response.BaseIsSuccess || !response.IsSuccess)
            {
                AlertHelper.ShowAlertMessage(response, this);
                UserDialogs.Instance.HideLoading();
                return;
            }

            UserDialogs.Instance.HideLoading();

            await Navigation.PushAsync(new LoginPageView());
        }
    }
}