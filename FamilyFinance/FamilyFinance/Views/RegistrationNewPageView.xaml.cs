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
    public partial class RegistrationNewPageView : ContentPage
    {
        private APIClient _apiClient;

        private Field _familyName;
        public Field FamilyName
        {
            get => _familyName;
            set
            {
                _familyName = value;
                OnPropertyChanged(nameof(FamilyName));
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

        public RegistrationNewPageView()
        {
            _apiClient = new APIClient();

            FamilyName = new Field();
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
                Login.NotValidMessageError = "Тільки латинські символи. Мінімум 4, максимум 30";
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
                Password.NotValidMessageError = "Тільки латинські символи. Мінімум 4, максимум 30";
                Password.IsNotValid = true;
            }
            else
            {
                Password.IsNotValid = false;
            }

            if (string.IsNullOrEmpty(FamilyName.Name))
            {
                FamilyName.NotValidMessageError = "Обов'язкове поле";
                FamilyName.IsNotValid = true;
            }
            else if (!Regex.IsMatch(FamilyName.Name, Constants.NameLatinAndCyrylicPattern))
            {
                FamilyName.NotValidMessageError = "Некоректне ім'я. Мінімум 4, максимум 30";
                FamilyName.IsNotValid = true;
            }
            else
            {
                FamilyName.IsNotValid = false;
            }

            if (string.IsNullOrEmpty(PersonName.Name))
            {
                PersonName.NotValidMessageError = "Обов'язкове поле";
                PersonName.IsNotValid = true;
            }
            else if (!Regex.IsMatch(PersonName.Name, Constants.NameLatinAndCyrylicPattern))
            {
                PersonName.NotValidMessageError = "Некоректне ім'я. Мінімум 4, максимум 30";
                PersonName.IsNotValid = true;
            }
            else
            {
                PersonName.IsNotValid = false;
            }

            if(!FamilyName.IsNotValid &&
                !PersonName.IsNotValid &&
                !Login.IsNotValid &&
                !Password.IsNotValid)
            {
                RegistrationAsync();
            }
        }

        private async void RegistrationAsync()
        {
            var request = new RegistrationRequest
            {
                FamilyName = FamilyName.Name.TrimEnd(),
                PersonName = PersonName.Name.TrimEnd(),
                Login = Login.Name.TrimEnd(),
                Password = Password.Name.TrimEnd(),
            };
            var response = await _apiClient.RegistrationNewAsync(request);
            if(!response.BaseIsSuccess || !response.IsSuccess)
            {
                return;
            }

            await Navigation.PushAsync(new LoginPageView());
        }
    }
}