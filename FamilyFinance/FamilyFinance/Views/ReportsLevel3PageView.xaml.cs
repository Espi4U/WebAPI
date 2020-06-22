using FamilyFinance.Helpers;
using Shared.Models.Requests.BaseRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FamilyFinance.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportsLevel3PageView : ContentPage
    {
        private readonly APIClient _apiClient;

        private DateTime _start;
        public DateTime Start
        {
            get => _start;
            set
            {
                _start = value;
                OnPropertyChanged(nameof(Start));
            }
        }

        private DateTime _end;
        public DateTime End
        {
            get => _end;
            set
            {
                _end = value;
                OnPropertyChanged(nameof(End));
            }
        }

        public DateTime Max
        {
            get => DateTime.Now;
        }

        public ICommand GenerateReportPerTimePeriodCommand { get; }

        public ReportsLevel3PageView()
        {
            _apiClient = new APIClient();

            Start = DateTime.Now;
            End = DateTime.Now;

            GenerateReportPerTimePeriodCommand = new Command(GenerateReportPerTimePeriodAsync);

            BindingContext = this;
            InitializeComponent();
        }

        private async void GenerateReportPerTimePeriodAsync()
        {
            var request = new GetResultsForTimePeriodRequest
            {
                Start = Start,
                End = End,
                Now = DateTime.Now,
                FamilyId = GlobalHelper.GetFamilyId(),
                PersonId = GlobalHelper.GetPersonId()
            };
            var response = await _apiClient.GenerateReportPerTimePeriod(request);
            if(!response.IsSuccess || !response.BaseIsSuccess)
            {
                AlertHelper.ShowAlertMessage(response, this);
                return;
            }

            await Navigation.PopAsync();
        }
    }
}