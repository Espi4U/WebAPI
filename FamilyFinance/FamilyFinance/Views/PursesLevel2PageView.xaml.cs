using Acr.UserDialogs;
using FamilyFinance.Helpers;
using FamilyFinance.Models;
using Newtonsoft.Json;
using Shared.Models.Requests.PursesRequests;
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
    public partial class PursesLevel2PageView : ContentPage
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

        private bool _isFamilyPurse = false;
        public bool IsFamilyPurse
        {
            get => _isFamilyPurse;
            set
            {
                _isFamilyPurse = value;
                OnPropertyChanged(nameof(IsFamilyPurse));
            }
        }

        private bool _isSeeIsFamilyPurseSwitch = false;
        public bool IsSeeIsFamilyPurseSwitch
        {
            get => _isSeeIsFamilyPurseSwitch;
            set
            {
                _isSeeIsFamilyPurseSwitch = value;
                OnPropertyChanged(nameof(IsSeeIsFamilyPurseSwitch));
            }
        }

        public ICommand OnValidationCommand { get; }

        public PursesLevel2PageView()
        {
            _apiClient = new APIClient();

            Name = new Field();
            IsSeeIsFamilyPurseSwitch = GlobalHelper.GetRole() == "H" ? true : false;

            OnValidationCommand = new Command(Validation);

            BindingContext = this;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                UserDialogs.Instance.ShowLoading();
                LoadCurrenciesAsync();
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
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
                SavePurseAsync();
            }
        }

        private async void SavePurseAsync()
        {
            UserDialogs.Instance.ShowLoading();
            var request = new PurseRequest
            {
                Name = Name.Name,
                Size = 0,
                CurrencyId = Currency.Id,
                FamilyId = GlobalHelper.GetFamilyId(),
            };
            if (IsFamilyPurse)
            {
                request.PersonId = null;
            }
            else
            {
                request.PersonId = GlobalHelper.GetPersonId();
            }

            var response = await _apiClient.AddPurseAsync(request);
            if(!response.BaseIsSuccess || !response.IsSuccess)
            {
                AlertHelper.ShowAlertMessage(response, this);
                UserDialogs.Instance.HideLoading();
                return;
            }
            UserDialogs.Instance.HideLoading();

            await Navigation.PopAsync();
        }

        private async void LoadCurrenciesAsync()
        {
            var response = await _apiClient.GetCurrenciesAsync(GlobalHelper.GetBaseRequest());
            if (!response.BaseIsSuccess || !response.IsSuccess)
            {
                AlertHelper.ShowAlertMessage(response, this);
                return;
            }

            Currencies = response.Currencies;
        }
    }
}