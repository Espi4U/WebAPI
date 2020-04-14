using FamilyFinance.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Models.APIModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FamilyFinance.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PursesLevel1PageView : ContentPage
    {
        private APIClient _apiClient;
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

        public PursesLevel1PageView()
        {
            _apiClient = new APIClient();

            BindingContext = this;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            LoadPursesAsync();
        }

        private async void LoadPursesAsync()
        {
            var response = await _apiClient.GetPursesAsync(GlobalHelper.GetBaseRequest());
            if(!response.BaseIsSuccess || !response.IsSuccess)
            {
                return;
            }

            Purses = response.Purses;
        }
    }
}