using FamilyFinance.Helpers;
using Shared.Models.Requests.CategoriesRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WebAPI.Models.APIModels;
using WebAPI.Models.APIModels.Requests;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FamilyFinance.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoriesLevel1PageView : ContentPage
    {
        private APIClient _apiClient;

        public Category SelectedCategory => null;

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

        public ICommand DeleteCommand { get; }

        public CategoriesLevel1PageView()
        {
            _apiClient = new APIClient();

            DeleteCommand = new Command(DeleteAsync);

            BindingContext = this;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            LoadCategoriesAsync();
        }

        private async void DeleteAsync(object parameter)
        {
            Category category = (Category)parameter;

            var request = new CategoryRequest
            {
                Category = category
            };
            var responce = await _apiClient.DeleteCategoryAsync(request);
            if(!responce.BaseIsSuccess || !responce.IsSuccess)
            {
                return;
            }
        }

        private async void LoadCategoriesAsync()
        {
            var response = await _apiClient.GetCategoriesAsync(GlobalHelper.GetBaseRequest());
            if(!response.BaseIsSuccess || !response.IsSuccess)
            {
                return;
            }

            Categories = response.Categories;
        }
    }
}