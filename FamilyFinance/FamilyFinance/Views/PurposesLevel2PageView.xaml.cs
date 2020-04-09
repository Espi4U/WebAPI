using Shared.Models.Requests.PurposesRequests;
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
    public partial class PurposesLevel2PageView : ContentPage
    {
        private APIClient _apiClient;
        public Purpose Purpose { get; set; }

        public ICommand EnlargeCommand { get; }
        public ICommand DeleteCommand { get; }

        public PurposesLevel2PageView(Purpose purpose)
        {
            _apiClient = new APIClient();

            Purpose = purpose;

            EnlargeCommand = new Command(EnlargeAsync);
            DeleteCommand = new Command(DeleteAsync);

            BindingContext = this;
            InitializeComponent();
        }

        private async void EnlargeAsync()
        {
            //goto edit page
        }

        private async void DeleteAsync()
        {
            var request = new PurposeRequest
            {
                Purpose = Purpose
            };
            var response = await _apiClient.DeletePurposeAsync(request);
            if(!response.BaseIsSuccess || !response.IsSuccess)
            {
                return;
            }

        }
    }
}