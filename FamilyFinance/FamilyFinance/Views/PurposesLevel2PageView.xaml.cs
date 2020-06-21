using FamilyFinance.Helpers;
using Shared.Models;
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
        public Purpose Purpose { get; set; }

        public ICommand EnlargePurposeCommand { get; }

        public PurposesLevel2PageView(Purpose purpose)
        {
            Purpose = purpose;

            EnlargePurposeCommand = new Command(EnlargePurposeAsync);

            BindingContext = this;
            InitializeComponent();
        }

        private async void EnlargePurposeAsync()
        {
            await Navigation.PushAsync(new PurposesLevel4PageView(Purpose.CurrencyId, Purpose.Id, Purpose.FinalSize, Purpose.CurrentSize));
        }
    }
}