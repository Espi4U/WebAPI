using Acr.UserDialogs;
using FamilyFinance.Helpers;
using FamilyFinance.Models;
using Shared.Models.Requests.CategoriesRequests;
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
    public partial class CategoriesLevel2PageView : ContentPage
    {
        private readonly APIClient _apiClient;

        private Field _name;
        public Field Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public ICommand OnValidationCommand { get; set; }

        public CategoriesLevel2PageView()
        {
            _apiClient = new APIClient();

            Name = new Field();

            OnValidationCommand = new Command(Validation);

            BindingContext = this;
            InitializeComponent();
        }

        private void Validation()
        {
            if (string.IsNullOrEmpty(Name.Name))
            {
                Name.NotValidMessageError = "Обов'язкове поле";
                Name.IsNotValid = true;
            }
            else if (!Regex.IsMatch(Name.Name, Constants.NameLatinAndCyrylicPattern))
            {
                Name.NotValidMessageError = "Тільки латинські символи. Мінімум 4, максимум 10";
                Name.IsNotValid = true;
            }
            else
            {
                Name.IsNotValid = false;
                AddNewCategoryAsync();
            }
        }

        private async void AddNewCategoryAsync()
        {
            UserDialogs.Instance.ShowLoading();
            var request = new CategoryRequest
            {
                Category = new Category
                {
                    Name = Name.Name,
                    FamilyId = GlobalHelper.GetFamilyId()
                }
            };

            var response = await _apiClient.AddCategoryAsync(request);
            if (!response.BaseIsSuccess || !response.IsSuccess)
            {
                AlertHelper.ShowAlertMessage(response, this);
                UserDialogs.Instance.HideLoading();
                return;
            }

            UserDialogs.Instance.HideLoading();
            await Navigation.PopAsync();
        }
    }
}