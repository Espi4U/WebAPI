using FamilyFinance.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FamilyFinance
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPageView();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
