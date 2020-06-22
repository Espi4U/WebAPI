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
using WebAPI.Models.APIModels.Requests;
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
        public ICommand GoToChartPageCommand { get; }

        public ReportsLevel1PageView()
        {
            _apiClient = new APIClient();

            GoToGenerateReportPageCommand = new Command(GoToGenerateReportPageAsync);
            GoToChartPageCommand = new Command(GoToChartPageAsync);

            BindingContext = this;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            LoadReportsAsync();
        }

        private async void GoToChartPageAsync()
        {
            await Navigation.PushAsync(new ChartPageView());
        }

        private async void LoadReportsAsync()
        {
            var request = new BaseRequest
            {
                FamilyId = GlobalHelper.GetFamilyId()
            };
            if(GlobalHelper.GetRole() == "H")
            {
                request.PersonId = null;
            }
            else
            {
                request.PersonId = GlobalHelper.GetPersonId();
            }
            var response = await _apiClient.GetReportsAsync(request);
            if(!response.BaseIsSuccess || !response.IsSuccess)
            {
                AlertHelper.ShowAlertMessage(response, this);
                return;
            }

            Reports = response.Reports;
        }

        private async void GoToGenerateReportPageAsync()
        {
            await Navigation.PushAsync(new ReportsLevel3PageView());
        }
    }
}