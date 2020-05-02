using FamilyFinance.Helpers;
using Shared.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FamilyFinance.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MePageView : ContentPage
    {
        private APIClient _apiClient;

        public ICommand SendInviteCommand { get; }

        private string _emailAddress;
        public string EmailAddress
        {
            get => _emailAddress;
            set
            {
                _emailAddress = value;
                OnPropertyChanged(nameof(EmailAddress));
            }
        }

        public MePageView()
        {
            _apiClient = new APIClient();

            SendInviteCommand = new Command(SendInvite);

            BindingContext = this;
            InitializeComponent();
        }

        private void SendInvite()
        {
            var key = GenerateNewInviteKey();

            SendEmailAsync(key);
            SaveKeyToDatabaseAsync(key);
        }

        private async void SendEmailAsync(string key)
        {
            try
            {
                var message = new EmailMessage
                {
                    Subject = "Hello",
                    Body = $"Use this invite key to registration: {key}",
                    To = new List<string> { EmailAddress },
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
        }
    }
}