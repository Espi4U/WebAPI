using FamilyFinance.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FamilyFinance.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPageView : ContentPage
    {
        public StartPageView()
        {

            BindingContext = this;
            InitializeComponent();
        }


        private void GoToNextPage()
        {
            if(GlobalHelper.GetFamilyId() == null ||
                GlobalHelper.GetPersonId() == null ||
                GlobalHelper.GetRole() == null ||
                GlobalHelper.GetPersonName() == null ||
                GlobalHelper.GetFamilyName() == null)
            {
                Navigation.PushAsync(new LoginPageView());
            }
            else
            {
                Navigation.PushAsync(new MainPageView());
            }
        }
    }
}