using Acr.UserDialogs;
using FamilyFinance.Helpers;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class PurposesLevel1PageView : ContentPage
    {
        private APIClient _apiClient;

        private List<Purpose> _purposes;
        public List<Purpose> Purposes
        {
            get => _purposes;
            set
            {
                _purposes = value;
                OnPropertyChanged(nameof(Purposes));
            }
        }

        public Purpose SelectedPurpose
        {
            get => null;
            set
            {
                if(value != null)
                {
                    OnPropertyChanged(nameof(SelectedPurpose));
                    Navigation.PushAsync(new PurposesLevel2PageView(value));
                }
            }
        }

        public ICommand AddNewPurposeCommand { get; }

        public PurposesLevel1PageView()
        {
            _apiClient = new APIClient();

            AddNewPurposeCommand = new Command(AddNewPurposeAsync);

            BindingContext = this;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            LoadPurposesAsync();
        }

        private async void LoadPurposesAsync()
        {
            UserDialogs.Instance.ShowLoading();
            var response = await _apiClient.GetPurposesAsync(GlobalHelper.GetBaseRequest());
            if(!response.BaseIsSuccess || !response.IsSuccess)
            {
                UserDialogs.Instance.HideLoading();
                AlertHelper.ShowAlertMessage(response, this);
                return;
            }

            Purposes = response.Purposes;
            UserDialogs.Instance.HideLoading();
        }

        private async void AddNewPurposeAsync()
        {
            await Navigation.PushAsync(new PurposesLevel3PageView());
        }
    }
}