using Acr.UserDialogs;
using FamilyFinance.Helpers;
using FamilyFinance.Models;
using Shared.Models;
using Shared.Models.Requests;
using Shared.Models.Requests.PurposesRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using WebAPI.Models.APIModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FamilyFinance.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PurposesLevel3PageView : ContentPage
    {
        private APIClient _apiClient;

        private Field _name;
        public Field Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private Field _finalSize;
        public Field FinalSize
        {
            get => _finalSize;
            set
            {
                _finalSize = value;
                OnPropertyChanged(nameof(FinalSize));
            }
        }

        private Currency _currency;
        public Currency Currency
        {
            get => _currency;
            set
            {
                _currency = value;
                OnPropertyChanged(nameof(Currency));
            }
        }

        private List<Currency> _currencies;
        public List<Currency> Currencies
        {
            get => _currencies;
            set
            {
                _currencies = value;
                OnPropertyChanged(nameof(Currencies));
            }
        }

        private bool _isFamilyPurpose;
        public bool IsFamilyPurpose
        {
            get => _isFamilyPurpose;
            set
            {
                _isFamilyPurpose = value;
                OnPropertyChanged(nameof(IsFamilyPurpose));
            }
        }

        private bool _isSeeIsFamilyPurposeSwitch = false;
        public bool IsSeeIsFamilyPurposeSwitch
        {
            get => _isSeeIsFamilyPurposeSwitch;
            set
            {
                _isSeeIsFamilyPurposeSwitch = value;
                OnPropertyChanged(nameof(IsSeeIsFamilyPurposeSwitch));
            }
        }

        public ICommand OnValidationCommand { get; }
        public ICommand ChangeIsFamilyPurposeStateCommand { get; }

        public PurposesLevel3PageView()
        {
            _apiClient = new APIClient();

            Name = new Field();
            FinalSize = new Field();

            IsSeeIsFamilyPurposeSwitch = GlobalHelper.GetRole() == "H" ? true : false;

            OnValidationCommand = new Command(Validation);
            ChangeIsFamilyPurposeStateCommand = new Command(ChangeIsFamilyPurposeState);

            BindingContext = this;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            LoadCurrenciesAsync();
        }

        private void ChangeIsFamilyPurposeState()
        {
            IsFamilyPurpose = !IsFamilyPurpose;
        }

        private async void SaveNewPurposeAsync()
        {
            var request = new PurposeRequest
            {
                Name = Name.Name,
                FinalSize = Convert.ToInt32(FinalSize.Name),
                CurrencyId = Currency.Id,
                FamilyId = GlobalHelper.GetFamilyId()
            };
            if (IsFamilyPurpose)
            {
                request.PersonId = null;
            }
            else
            {
                request.PersonId = GlobalHelper.GetPersonId();
            }
            var response = await _apiClient.AddPurposeAsync(request);
            if(!response.BaseIsSuccess || !response.IsSuccess)
            {
                return;
            }

            await Navigation.PopAsync();
        }

        private void Validation()
        {
            if (string.IsNullOrEmpty(Name.Name))
            {
                Name.NotValidMessageError = "Обов'язкове поле";
                Name.IsNotValid = true;
            }
            else if (!Regex.IsMatch(Name.Name, Constants.NameLatinAndCyrylicPattern))
            {
                Name.NotValidMessageError = "Некоректна назва. Мінімум 4, максимум 10";
                Name.IsNotValid = true;
            }
            else
            {
                Name.IsNotValid = false;
            }

            if (string.IsNullOrEmpty(FinalSize.Name))
            {
                FinalSize.NotValidMessageError = "Обов'язкове поле";
                FinalSize.IsNotValid = true;
            }
            else if (!Regex.IsMatch(FinalSize.Name, Constants.PositiveDigitsPattern))
            {
                FinalSize.NotValidMessageError = "Тільки додатні числа";
                FinalSize.IsNotValid = true;
            }
            else
            {
                FinalSize.IsNotValid = false;
            }

            if(!Name.IsNotValid &&
                !FinalSize.IsNotValid &&
                Currency != null)
            {
                SaveNewPurposeAsync();
            }
        }

        private async void LoadCurrenciesAsync()
        {
            var response = await _apiClient.GetCurrenciesAsync(GlobalHelper.GetBaseRequest());
            if(!response.BaseIsSuccess || !response.IsSuccess)
            {
                return;
            }

            Currencies = response.Currencies;
        }
    }
}