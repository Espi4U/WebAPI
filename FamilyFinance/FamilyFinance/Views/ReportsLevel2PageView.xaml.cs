using FamilyFinance.Helpers;
using Shared.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WebAPI.Models.APIModels;
using WebAPI.Models.APIModels.Requests.ReportsControllerRequests;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FamilyFinance.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportsLevel2PageView : ContentPage
    {
        private APIClient _apiClient;
        public ICommand DeleteReportCommand { get; set; }

        public Report Report { get; set; }

        public ReportsLevel2PageView(Report report)
        {
            _apiClient = new APIClient();

            Report = report;

            DeleteReportCommand = new Command(DeleteReportAsync);

            BindingContext = this;
            InitializeComponent();
        }

        private async void DeleteReportAsync()
        {
            var request = new DeleteReportRequest
            {
                ReportId = Report.Id,
                FamilyId = GlobalHelper.GetFamilyId(),
                PersonId = GlobalHelper.GetPersonId()
            };
            var response = await _apiClient.DeleteReportAsync(request);
            if (!response.IsSuccess || !response.BaseIsSuccess)
            {
                return;
            }

            await Navigation.PopAsync();
        }

    }
}