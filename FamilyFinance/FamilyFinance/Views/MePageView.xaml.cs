using Acr.UserDialogs;
using FamilyFinance.Helpers;
using Shared.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WebAPI.Models.APIModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FamilyFinance.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MePageView : ContentPage
    {
        private readonly APIClient _apiClient;

        public ChangeMoney SelectedChangeMoney
        {
            get => null;
            set
            {
                OnPropertyChanged(nameof(SelectedChangeMoney));
            }
        }

        private List<ChangeMoney> _changeMoneys;
        public List<ChangeMoney> ChangeMoneys
        {
            get => _changeMoneys;
            set
            {
                _changeMoneys = value;
                OnPropertyChanged(nameof(ChangeMoneys));
            }
        }

        public MePageView()
        {
            _apiClient = new APIClient();

            BindingContext = this;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            LoadChangeMoneysAsync();
        }

        private async void LoadChangeMoneysAsync()
        {
            var response = await _apiClient.GetAllIncomesOrExpensesAsync(GlobalHelper.GetBaseRequest());
            if(!response.BaseIsSuccess || !response.IsSuccess)
            {
                AlertHelper.ShowAlertMessage(response, this);
                return;
            }

            ChangeMoneys = response.ChangeMoneys;
        }
    }
}