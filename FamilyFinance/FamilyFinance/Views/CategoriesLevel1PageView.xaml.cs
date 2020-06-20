using Acr.UserDialogs;
using FamilyFinance.Helpers;
using Shared.Models.Requests.CategoriesRequests;
using Shared.Models.Requests.FamiliesRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WebAPI.Models.APIModels;
using WebAPI.Models.APIModels.Requests;
using WebAPI.Models.APIModels.Requests.PersonsControllerRequests;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FamilyFinance.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoriesLevel1PageView : ContentPage
    {
        private APIClient _apiClient;

        public Category SelectedCategory
        {
            get => null;
            set
            {
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }

        private List<Category> _categories;
        public List<Category> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }

        public ICommand AddNewCategoryCommand { get; }

        public CategoriesLevel1PageView()
        {
            _apiClient = new APIClient();

            AddNewCategoryCommand = new Command(AddNewCategoryAsync);

            BindingContext = this;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            LoadCategoriesAsync();
        }

        private async void LoadCategoriesAsync()
        {
            UserDialogs.Instance.ShowLoading();
            var response = await _apiClient.GetCategoriesAsync(GlobalHelper.GetBaseRequest());
            if(!response.BaseIsSuccess || !response.IsSuccess)
            {
                UserDialogs.Instance.HideLoading();
                AlertHelper.ShowAlertMessage(response, this);
                return;
            }

            Categories = response.Categories;
            UserDialogs.Instance.HideLoading();
        }

        private async void AddNewCategoryAsync()
        {
            await Navigation.PushAsync(new CategoriesLevel2PageView());
        }
    }
}