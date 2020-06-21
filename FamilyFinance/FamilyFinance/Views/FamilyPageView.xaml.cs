using FamilyFinance.Helpers;
using FamilyFinance.Models;
using Shared.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FamilyFinance.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FamilyPageView : ContentPage
    {
        private APIClient _apiClient;

        private Field _emailAddress;
        public Field EmailAddress
        {
            get => _emailAddress;
            set
            {
                _emailAddress = value;
                OnPropertyChanged(nameof(EmailAddress));
            }
        }

        public ICommand OnValidationCommand { get; }
        public ICommand LogoutCommand { get; }

        public FamilyPageView()
        {
            _apiClient = new APIClient();

            EmailAddress = new Field();

            OnValidationCommand = new Command(Validation);
            LogoutCommand = new Command(LogoutAsync);

            BindingContext = this;
            InitializeComponent();
        }

        private void Validation()
        {
            if (string.IsNullOrEmpty(EmailAddress.Name))
            {
                EmailAddress.NotValidMessageError = "Обов'язкове поле";
                EmailAddress.IsNotValid = true;
            }
            else if (!Regex.IsMatch(EmailAddress.Name, Constants.EmailPattern))
            {
                EmailAddress.NotValidMessageError = "Некоректна email адреса";
                EmailAddress.IsNotValid = true;
            }
            else
            {
                EmailAddress.IsNotValid = false;
                SendInvite();
            }
        }

        private void SendInvite()
        {
            var key = GenerateNewInviteKey();

            SendEmailAsync(key);
            SaveKeyToDatabaseAsync(key);
            EmailAddress.Name = string.Empty;
        }

        private async void SendEmailAsync(string key)
        {
            try
            {
                var message = new EmailMessage
                {
                    Subject = "Вітання!",
                    Body = $"Використайте цей код для реєстрації: {key}",
                    To = new List<string> { EmailAddress.Name },
                };
                await Email.ComposeAsync(message);
            }
            catch { }
        }

        private string GenerateNewInviteKey()
        {
            return Guid.NewGuid().ToString();
        }

        private async void SaveKeyToDatabaseAsync(string key)
        {
            var request = new InviteKeyRequest
            {
                FamilyId = GlobalHelper.GetFamilyId(),
                Key = key
            };
            var response = await _apiClient.AddInviteKey(request);
            if (!response.BaseIsSuccess || !response.IsSuccess)
            {
                return;
            }
        }

        private async void LogoutAsync()
        {
            GlobalHelper.Logout();
            await Navigation.PushAsync(new LoginPageView());
        }
    }
}