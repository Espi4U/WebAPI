using Acr.UserDialogs;
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
using WebAPI.Models.APIModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FamilyFinance.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PurposesLevel4PageView : ContentPage
    {
        private readonly APIClient _apiClient;

        private int CurrencyId { get; set; }
        private int PurposeId { get; set; }
        private int PurposeFinalSize { get; set; }
        private int PurposeCurrentSize { get; set; }

        private Field _newSize;
        public Field NewSize
        {
            get => _newSize;
            set
            {
                _newSize = value;
                OnPropertyChanged(nameof(NewSize));
            }
        }

        private Purse _purse;
        public Purse Purse
        {
            get => _purse;
            set
            {
                _purse = value;
                OnPropertyChanged(nameof(Purse));
            }
        }

        private List<Purse> _purses;
        public List<Purse> Purses
        {
            get => _purses;
            set
            {
                _purses = value;
                OnPropertyChanged(nameof(Purses));
            }
        }

        public ICommand OnValidationCommand { get; }

        public PurposesLevel4PageView(int currencyId, int purposeId, int purposeFinalSize, int purposeCurrentSize)
        {
            _apiClient = new APIClient();

            CurrencyId = currencyId;
            PurposeId = purposeId;
            PurposeFinalSize = purposeFinalSize;
            PurposeCurrentSize = purposeCurrentSize;

            NewSize = new Field();

            OnValidationCommand = new Command(Validation);

            BindingContext = this;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            LoadPursesByCurrencyIdAsync(CurrencyId);
        }

        private async void LoadPursesByCurrencyIdAsync(int currencyId)
        {
            var request = new GetPursesByCurrencyRequest
            {
                CurrencyId = currencyId,
                FamilyId = GlobalHelper.GetFamilyId(),
                PersonId = GlobalHelper.GetPersonId()
            };
            var response = await _apiClient.GetPursesByCurrencyAsync(request);
            if (!response.IsSuccess || !response.BaseIsSuccess)
            {
                return;
            }

            Purses = response.Purses;
        }

        private void Validation()
        {
            if (string.IsNullOrEmpty(NewSize.Name))
            {
                NewSize.NotValidMessageError = "Обов'язкове поле";
                NewSize.IsNotValid = true;
            }
            else if (!Regex.IsMatch(NewSize.Name, Constants.PositiveDigitsPattern))
            {
                NewSize.NotValidMessageError = "Тільки додатні числа";
                NewSize.IsNotValid = true;
            }
            else if(Purse.Size < Convert.ToInt32(NewSize.Name))
            {
                NewSize.NotValidMessageError = "Недостатньо коштів";
                NewSize.IsNotValid = true;
            }
            else if(PurposeCurrentSize + Convert.ToInt32(NewSize.Name) > PurposeFinalSize)
            {
                NewSize.NotValidMessageError = "Перевищення необхідної суми";
                NewSize.IsNotValid = true;
            }
            else
            {
                NewSize.IsNotValid = false;
                UpdatePurposeAsync();
            }
        }

        private async void UpdatePurposeAsync()
        {
            var request = new UpdatePurposeRequest
            {
                PersonId = GlobalHelper.GetPersonId(),
                FamilyId = GlobalHelper.GetFamilyId(),
                NewSize = Convert.ToInt32(NewSize.Name),
                PurposeId = PurposeId,
                PurseId = Purse.Id
            };
            var response = await _apiClient.UpdatePurposeAsync(request);
            if(!response.IsSuccess || !response.BaseIsSuccess)
            {
                return;
            }

            await Navigation.PopToRootAsync();
        }
    }
}