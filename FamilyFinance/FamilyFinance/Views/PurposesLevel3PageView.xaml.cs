using FamilyFinance.Helpers;
using Shared.Models;
using Shared.Models.Requests;
using Shared.Models.Requests.PurposesRequests;
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
    public partial class PurposesLevel3PageView : ContentPage
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

        private int _finalSize;
        public int FinalSize
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
                LoadPursesByCurrencyIdAsync(Currency.Id);
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

        public ICommand SaveCommand { get; }

        public PurposesLevel3PageView()
        {
            _apiClient = new APIClient();

            SaveCommand = new Command(SaveAsync);

            BindingContext = this;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            LoadCurrenciesAsync();
        }

        private async void SaveAsync()
        {
            var request = new PurposeRequest
            {
                Name = Name,
                FinalSize = FinalSize,
                CurrencyId = Currency.Id,
                FamilyId = GlobalHelper.GetFamilyId(),
                PersonId = GlobalHelper.GetPersonId()
            };
            var response = await _apiClient.AddPurposeAsync(request);
            if(!response.BaseIsSuccess || !response.IsSuccess)
            {
                AlertHelper.ShowAlertMessage(response, this);
                return;
            }

            await Navigation.PopAsync();
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
            if (!response.IsSuccess || !response.BaseIsSuccess)
            {
                AlertHelper.ShowAlertMessage(response, this);
                return;
            }

            Purses = response.Purses;
        }
    }
}