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

            GoToNextPage();
            InitializeComponent();
        }


        private void GoToNextPage()
        {
            if(GlobalHelper.GetFamilyId() == 0 ||
                GlobalHelper.GetPersonId() == 0 ||
                GlobalHelper.GetRole() == "" ||
                GlobalHelper.GetPersonName() == "" ||
                GlobalHelper.GetFamilyName() == "")
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