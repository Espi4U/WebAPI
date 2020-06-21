using FamilyFinance.Helpers;
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
    public partial class UserInfoPageView : ContentPage
    {
        public string FamilyName => $"Сім'я: {GlobalHelper.GetFamilyName()}";
        public string UserName => $"Ваше ім'я: {GlobalHelper.GetPersonName()}";
        public string Role => GlobalHelper.GetRole() == "H" ? "Ваше становище в сім'ї: Голова сім'ї" : "Ваше становище в сім'ї: Звичайний член сім'ї";

        public ICommand LogoutCommand { get; }

        public UserInfoPageView()
        {
            LogoutCommand = new Command(LogoutAsync);

            BindingContext = this;
            InitializeComponent();
        }

        private async void LogoutAsync()
        {
            GlobalHelper.Logout();
            await Navigation.PushAsync(new LoginPageView());
        }
    }
}