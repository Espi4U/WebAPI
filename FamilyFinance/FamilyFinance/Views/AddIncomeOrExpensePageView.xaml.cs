using Acr.UserDialogs;
using FamilyFinance.Helpers;
using FamilyFinance.Models;
using Shared.Models.Requests;
using Shared.Models.Requests.ChangeMoneyRequests;
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
    public partial class AddIncomeOrExpensePageView : ContentPage
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

        private Field _size;
        public Field Size
        {
            get => _size;
            set
            {
                _size = value;
                OnPropertyChanged(nameof(Size));
            }
        }

        private bool _isIncome;
        public bool IsIncome
        {
            get => _isIncome;
            set
            {
                _isIncome = value;
                OnPropertyChanged(nameof(IsIncome));
            }
        }

        private Category _category;
        public Category Category
        {
            get => _category;
            set
            {
                _category = value;
                OnPropertyChanged(nameof(Category));
            }
        }

        private Currency _currency;
        public Currency Currency
        {
            get => _currency;
            set
            {
                _currency = value;
                Category = null;
                LoadPursesByCurrencyIdAsync(Currency.Id);
                OnPropertyChanged(nameof(Currency));
            }
        }

        private Purse _purse;
        public Purse Purse
        {
            get => _purse;
            set
            {
                _purse = value;
                OnPropertyChanged(nameof(Purse));
            }
        }

        private List<Category> _categories;
        public List<Category> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged(nameof(Categories));
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

        private List<Purse> _purses;
        public List<Purse> Purses
        {
            get => _purses;
            set
            {
                _purses = value;
                OnPropertyChanged(nameof(Purses));
            }
        }

        public ICommand OnValidationCommand { get; }
        public ICommand ChangeIsIncomeStateCommand { get; }

        public AddIncomeOrExpensePageView()
        {
            _apiClient = new APIClient();

            Name = new Field();
            Size = new Field();

            OnValidationCommand = new Command(Validation);
            ChangeIsIncomeStateCommand = new Command(ChangeIsIncomeState);

            BindingContext = this;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            LoadCategoriesAsync();
            LoadCurrenciesAsync();
        }

        private void ChangeIsIncomeState()
        {
            IsIncome = !IsIncome;
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

            if (string.IsNullOrEmpty(Size.Name))
            {
                Size.NotValidMessageError = "Обов'язкове поле";
                Size.IsNotValid = true;
            }
            else if (!Regex.IsMatch(Size.Name, Constants.PositiveDigitsPattern))
            {
                Size.NotValidMessageError = "Тільки додатні числа";
                Size.IsNotValid = true;
            }
            else if(!IsIncome && Convert.ToInt32(Size.Name) > Purse.Size)
            {
                Size.NotValidMessageError = "Недостатньо коштів";
                Size.IsNotValid = true;
            }
            else
            {
                Size.IsNotValid = false;
            }

            if (!Name.IsNotValid &&
                !Size.IsNotValid &&
                Currency != null &&
                Category != null &&
                Purse != null)
            {
                AddNewIncomeOrExpenseAsync();
            }
        }

        private async void AddNewIncomeOrExpenseAsync()
        {
            UserDialogs.Instance.ShowLoading();
            var request = new ChangeMoneyRequest
            {
                Name = Name.Name,
                Size = Convert.ToInt32(Size.Name),
                Type = IsIncome ? "I" : "E",
                Date = DateTime.Now,
                Category = Category,
                Currency = Currency,
                Purse = Purse,
                FamilyId = GlobalHelper.GetFamilyId(),
                PersonId = GlobalHelper.GetPersonId()
            };
            var response = await _apiClient.AddIncomeOrExpenseAsync(request);
            if(!response.BaseIsSuccess || !response.IsSuccess)
            {
                UserDialogs.Instance.HideLoading();
                AlertHelper.ShowAlertMessage(response, this);
                return;
            }

            UserDialogs.Instance.HideLoading();

            await Navigation.PopAsync();
        }

        private async void LoadCategoriesAsync()
        {
            UserDialogs.Instance.ShowLoading();
            var response = await _apiClient.GetCategoriesAsync(GlobalHelper.GetBaseRequest());
            if (!response.BaseIsSuccess || !response.IsSuccess)
            {
                UserDialogs.Instance.HideLoading();
                AlertHelper.ShowAlertMessage(response, this);
                return;
            }

            Categories = response.Categories;
            UserDialogs.Instance.HideLoading();
        }

        private async void LoadCurrenciesAsync()
        {
            UserDialogs.Instance.ShowLoading();
            var response = await _apiClient.GetCurrenciesAsync(GlobalHelper.GetBaseRequest());
            if (!response.BaseIsSuccess || !response.IsSuccess)
            {
                UserDialogs.Instance.HideLoading();
                AlertHelper.ShowAlertMessage(response, this);
                return;
            }

            Currencies = response.Currencies;
            UserDialogs.Instance.HideLoading();
        }

        private async void LoadPursesByCurrencyIdAsync(int currencyId)
        {
            UserDialogs.Instance.ShowLoading();
            var request = new GetPursesByCurrencyRequest
            {
                CurrencyId = currencyId,
                FamilyId = GlobalHelper.GetFamilyId(),
                PersonId = GlobalHelper.GetPersonId()
            };
            var response = await _apiClient.GetPursesByCurrencyAsync(request);
            if(!response.IsSuccess || !response.BaseIsSuccess)
            {
                AlertHelper.ShowAlertMessage(response, this);
                UserDialogs.Instance.HideLoading();
                return;
            }

            UserDialogs.Instance.HideLoading();

            Purses = response.Purses;
        }
    }
}