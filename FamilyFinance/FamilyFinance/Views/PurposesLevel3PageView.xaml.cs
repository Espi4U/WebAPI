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

        private Purpose _purpose;
        public Purpose Purpose
        {
            get => _purpose;
            set
            {
                _purpose = value;
                LoadPursesAsync(Purpose.CurrencyId);
                OnPropertyChanged(nameof(Purpose));
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
        public ICommand DeleteCommand { get; }

        public PurposesLevel3PageView(Purpose purpose = null)
        {
            _apiClient = new APIClient();
            Purpose = new Purpose
            {
                FamilyId = GlobalHelper.GetFamilyId(),
                PersonId = GlobalHelper.GetPersonId()
            };

            if(purpose == null)
            {
                SaveCommand = new Command(SaveAsync);
            }
            else
            {
                Purpose = purpose;
                SaveCommand = new Command(UpdateAsync);
            }

            DeleteCommand = new Command(DeleteAsync);

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
            this.Purpose.Currency = new Currency { Name = "Українська гривня" };
            var request = new PurposeRequest
            {
                Purpose = Purpose
            };
            var response = await _apiClient.AddPurposeAsync(request);
            if(!response.BaseIsSuccess || !response.IsSuccess)
            {
                AlertHelper.ShowAlertMessage(response, this);
                return;
            }

            await Navigation.PopAsync();
        }

        private async void UpdateAsync()
        {
            var request = new PurposeRequest
            {
                Purpose = Purpose
            };
            var response = await _apiClient.UpdatePurposeAsync(request);
            if (!response.BaseIsSuccess || !response.IsSuccess)
            {
                AlertHelper.ShowAlertMessage(response, this);
                return;
            }

            await Navigation.PopAsync();
        }

        private async void DeleteAsync()
        {
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

        private async void LoadPursesAsync(int currencyId)
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