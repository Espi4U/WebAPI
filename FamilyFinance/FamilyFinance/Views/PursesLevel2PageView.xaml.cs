using Acr.UserDialogs;
using FamilyFinance.Helpers;
using Newtonsoft.Json;
using Shared.Models.Requests.PursesRequests;
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
    public partial class PursesLevel2PageView : ContentPage
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

        public ICommand SavePurseCommand { get; }

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

        public PursesLevel2PageView()
        {
            _apiClient = new APIClient();

            SavePurseCommand = new Command(SavePurseAsync);

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

        private async void SavePurseAsync()
        {
            UserDialogs.Instance.ShowLoading();
            var request = new PurseRequest
            {
                Name = Name,
                Size = 0,
                CurrencyId = Currency.Id,
                FamilyId = GlobalHelper.GetFamilyId(),
                PersonId = GlobalHelper.GetPersonId()
            };
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