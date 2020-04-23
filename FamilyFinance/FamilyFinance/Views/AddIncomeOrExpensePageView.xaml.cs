using FamilyFinance.Helpers;
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

        private ChangeMoney _changeMoney;
        public ChangeMoney ChangeMoney
        {
            get => _changeMoney;
            set
            {
                _changeMoney = value;
                OnPropertyChanged(nameof(ChangeMoney));
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

        public ICommand AddCommand { get; }

        public AddIncomeOrExpensePageView()
        {
            _apiClient = new APIClient();

            ChangeMoney = new ChangeMoney
            {
                FamilyId = GlobalHelper.GetFamilyId(),
                PersonId = GlobalHelper.GetPersonId()
            };

            AddCommand = new Command(AddAsync);

            BindingContext = this;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            LoadCategoriesAsync();
            LoadCurrenciesAsync();
        }

        private async void AddAsync()
        {
            var request = new ChangeMoneyRequest
            {
                ChangeMoney = ChangeMoney
            };
            // update api client
        }

        private async void LoadCategoriesAsync()
        {
            var response = await _apiClient.GetCategoriesAsync(GlobalHelper.GetBaseRequest());
            if (!response.BaseIsSuccess || !response.IsSuccess)
            {
                return;
            }

            Categories = response.Categories;
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