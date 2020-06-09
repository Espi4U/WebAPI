using FamilyFinance.Helpers;
using Shared.Models.Requests;
using Shared.Models.Requests.ChangeMoneyRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private int _size;
        public int Size
        {
            get => _size;
            set
            {
                _size = value;
                OnPropertyChanged(nameof(Size));
            }
        }

        public int MaxSliderOrStepperValue
        {
            get
            {
                if (Purse != null)
                {
                    return IsIncome ? 5000 : Purse.Size;
                }
                else
                {
                    return 1;
                }
            }
        }

        private bool _isIncome;
        public bool IsIncome
        {
            get => _isIncome;
            set
            {
                _isIncome = value;
                Size = 0;
                OnPropertyChanged(nameof(IsIncome));
                OnPropertyChanged(nameof(MaxSliderOrStepperValue));
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
                OnPropertyChanged(nameof(MaxSliderOrStepperValue));
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

        public ICommand AddNewIncomeOrExpenseCommand { get; }

        public AddIncomeOrExpensePageView()
        {
            _apiClient = new APIClient();

            AddNewIncomeOrExpenseCommand = new Command(AddNewIncomeOrExpenseAsync);

            BindingContext = this;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            LoadCategoriesAsync();
            LoadCurrenciesAsync();
        }

        private async void AddNewIncomeOrExpenseAsync()
        {
            var request = new ChangeMoneyRequest
            {
                Name = Name,
                Size = Size,
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
                AlertHelper.ShowAlertMessage(response, this);
                return;
            }
        }

        private async void LoadCategoriesAsync()
        {
            var response = await _apiClient.GetCategoriesAsync(GlobalHelper.GetBaseRequest());
            if (!response.BaseIsSuccess || !response.IsSuccess)
            {
                AlertHelper.ShowAlertMessage(response, this);
                return;
            }

            Categories = response.Categories;
        }

        private async void LoadCurrenciesAsync()
        {
            var response = await _apiClient.GetCurrenciesAsync(GlobalHelper.GetBaseRequest());
            if(!response.BaseIsSuccess || !response.IsSuccess)
            {
                AlertHelper.ShowAlertMessage(response, this);
                return;
            }

            Currencies = response.Currencies;
        }

        private async void LoadPursesByCurrencyIdAsync(int currencyId)
        {
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
                return;
            }

            Purses = response.Purses;
        }
    }
}