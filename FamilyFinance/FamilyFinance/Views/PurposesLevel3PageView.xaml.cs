using FamilyFinance.Helpers;
using FamilyFinance.Models;
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

        public ICommand SaveCommand { get; }

        public PurposesLevel3PageView(PurposesLevel3PageViewModes mode)
        {
            _apiClient = new APIClient();
            Purpose = new Purpose
            {
                FamilyId = GlobalHelper.GetFamilyId(),
                PersonId = GlobalHelper.GetPersonId()
            };

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
                Purpose = Purpose
            };
            var response = await _apiClient.AddPurposeAsync(request);
            if(!response.BaseIsSuccess || !response.IsSuccess)
            {
                return;
            }

            await Navigation.PopAsync();
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