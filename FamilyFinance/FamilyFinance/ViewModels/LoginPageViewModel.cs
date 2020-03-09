using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FamilyFinance.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        public ICommand LogInCommand { get; set; }
        public ICommand LogOutCommand { get; set; }
        public ICommand RegistrationCommand { get; set; }
        public ICommand ResetAllFieldsCommand { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }
        public int PINCode { get; set; }

        public LoginPageViewModel()
        {
            LogInCommand = new Command(LogIn);
            LogOutCommand = new Command(LogOut);
            RegistrationCommand = new Command(Registration);
            ResetAllFieldsCommand = new Command(ResetAllFields);
        }

        private void LogIn()
        {

        }

        private void LogOut()
        {

        }

        private void Registration()
        {

        }

        private void ResetAllFields()
        {
            Login = default;
            Password = default;
            PINCode = default;
        } 
    }
}
