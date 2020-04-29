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

        public ICommand SaveCommand { get; }

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

        public PursesLevel2PageView(List<Currency> currencies)
        {
            _apiClient = new APIClient();

            Currencies = currencies;

            SaveCommand = new Command(SaveAsync);

            Purse = new Purse
            {
                FamilyId = GlobalHelper.GetFamilyId(),
                PersonId = GlobalHelper.GetPersonId()
            };

            BindingContext = this;
            InitializeComponent();
        }

        private async void SaveAsync()
        {
            var request = new PurseRequest
            {
                Purse = Purse
            };
            var response = await _apiClient.AddPurseAsync(request);
            if(!response.BaseIsSuccess || !response.IsSuccess)
            {
                AlertHelper.ShowAlertMessage(response, this);
                return;
            }

            await Navigation.PopAsync();
        }

        public string GetNameByCurrencyId(int id)
        {
            return Currencies.Where(x=> x.Id == id).FirstOrDefault().Name;
        }
    }
}