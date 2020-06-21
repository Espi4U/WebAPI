using Acr.UserDialogs;
using FamilyFinance.Helpers;
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
    public partial class ReportsLevel1PageView : ContentPage
    {
        private APIClient _apiClient;

        private List<Report> _reports;
        public List<Report> Reports
        {
            get => _reports;
            set
            {
                _reports = value;
                OnPropertyChanged(nameof(Reports));
            }
        }

        public Report SelectedReport
        {
            get => null;
            set
            {
                if (value != null)
                {
                    OnPropertyChanged(nameof(SelectedReport));
                    Navigation.PushAsync(new ReportsLevel2PageView(value));
                }
            }
        }

        public ICommand GoToGenerateReportPageCommand { get; }

        public ReportsLevel1PageView()
        {
            _apiClient = new APIClient();

            GoToGenerateReportPageCommand = new Command(GoToGenerateReportPage);

            BindingContext = this;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            LoadReportsAsync();
        }

        private async void LoadReportsAsync()
        {
            var response = await _apiClient.GetReportsAsync(GlobalHelper.GetBaseRequest());
            if(!response.BaseIsSuccess || !response.IsSuccess)
            {
                AlertHelper.ShowAlertMessage(response, this);
                return;
            }

            Reports = response.Reports;
        }

        private void GoToGenerateReportPage()
        {
            //Navigation.PushAsync(page)
        }
    }
}