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
    public partial class LoginPageView : ContentPage
    {
        private APIClient _apiClient;
        public string Login { get; set; }

        public string Password { get; set; }

        public int PINCode { get; set; }

        public ICommand LogInCommand { get; }

        public LoginPageView()
        {
            _apiClient = new APIClient();

            LogInCommand = new Command(LogIn);

            BindingContext = this;
            InitializeComponent();
        }

        public void LogIn()
        {

        }
    }
}