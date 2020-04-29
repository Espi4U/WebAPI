using FamilyFinance.Helpers;
using Newtonsoft.Json;
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
    public partial class PursesLevel1PageView : ContentPage
    {
        private APIClient _apiClient;

        public ICommand AddCommand { get; }

        public Purse SelectedPurse
        {
            get => null;
            set { OnPropertyChanged(nameof(SelectedPurse)); }
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

        public PursesLevel1PageView()
        {
            _apiClient = new APIClient();

            AddCommand = new Command(AddAsync);

            BindingContext = this;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            LoadCurrenciesAsync();
            LoadPursesAsync();
        }

        private async void LoadPursesAsync()
        {
            var response = await _apiClient.GetPursesAsync(GlobalHelper.GetBaseRequest());
            if (!response.BaseIsSuccess || !response.IsSuccess)
            {
                AlertHelper.ShowAlertMessage(response, this);
                return;
            }

            Purses = response.Purses;
        }

        private async void AddAsync()
        {
            await Navigation.PushAsync(new PursesLevel2PageView(Currencies));
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

        public string CurrencyIdToName(int id)
        {
            return Currencies.Where(x => x.Id == id).FirstOrDefault().Name;
        }
    }
}