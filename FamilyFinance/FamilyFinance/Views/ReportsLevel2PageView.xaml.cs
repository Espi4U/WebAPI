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
        public ICommand DeleteCommand { get; set; }

        public Report Report { get; set; }

        public ReportsLevel2PageView(Report report)
        {
            _apiClient = new APIClient();
            Report = report;
            DeleteCommand = new Command(DeleteAsync);

            BindingContext = this;
            InitializeComponent();
        }

        public async void DeleteAsync()
        {
            var request = new ReportRequest
            {
                Report = Report
            };
            var responce = await _apiClient.DeleteReportAsync(request);
            if(!responce.BaseIsSuccess || !responce.IsSuccess)
            {
                AlertHelper.ShowAlertMessage(response, this);
                return;
            }

        }
    }
}