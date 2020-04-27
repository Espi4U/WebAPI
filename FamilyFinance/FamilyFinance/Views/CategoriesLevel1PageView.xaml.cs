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

        public ICommand DeleteCommand { get; }
        public ICommand AddCommand { get; }

        public CategoriesLevel1PageView()
        {
            _apiClient = new APIClient();

            DeleteCommand = new Command(DeleteAsync);
            AddCommand = new Command(AddAsync);

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
            var response = await _apiClient.DeleteCategoryAsync(request);
            if(!response.BaseIsSuccess || !response.IsSuccess)
            {
                AlertHelper.ShowAlertMessage(response, this);
                return;
            }
        }

        private async void LoadCategoriesAsync()
        {
            var response = await _apiClient.GetCategoriesAsync(GlobalHelper.GetBaseRequest());
            if(!response.BaseIsSuccess || !response.IsSuccess)
            {
                AlertHelper.ShowAlertMessage(response, this);
                return;
            }

            Categories = response.Categories;
        }

        private async void AddAsync()
        {
            var result = await DisplayPromptAsync("Enter Category Name", "Please Enter A New Category Name");
            if (!string.IsNullOrWhiteSpace(result))
            {
                var request = new CategoryRequest
                {
                    Category = new Category
                    {
                        Name = result
                    }
                };

                var response = await _apiClient.AddCategoryAsync(request);
                if (!response.BaseIsSuccess || !response.IsSuccess)
                {
                    AlertHelper.ShowAlertMessage(response, this);
                    return;
                }

                LoadCategoriesAsync();
            }
        }
    }
}