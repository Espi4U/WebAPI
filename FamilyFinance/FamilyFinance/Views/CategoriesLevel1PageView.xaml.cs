using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WebAPI.Models.APIModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FamilyFinance.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoriesLevel1PageView : ContentPage
    {
        public Category SelectedCategory => null;
        public List<Category> Categories { get; set; }

        public ICommand DeleteCommand { get; }

        public CategoriesLevel1PageView()
        {
            DeleteCommand = new Command(Delete);

            Categories = new List<Category>
            {
                new Category(){ Name = "Cat 1"},
                new Category(){ Name = "Cat 2"},
                new Category(){ Name = "Cat 3"},
                new Category(){ Name = "Cat 4"},
                new Category(){ Name = "Cat 5"},
                new Category(){ Name = "Cat 6"},
                new Category(){ Name = "Cat 7"},
                new Category(){ Name = "Cat 8"},
                new Category(){ Name = "Cat 9"},
            };

            BindingContext = this;
            InitializeComponent();
        }

        private void Delete(object parameter)
        {
            int id = (int)parameter;
            //call API here
        }
    }
}