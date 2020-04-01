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
    public partial class ListPageView : ContentPage
    {
        public ICommand NavigateToCommand { get; }

        public ListPageView()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            NavigateToCommand = new Command(NavigateTo);

            BindingContext = this;
            InitializeComponent();
        }

        private void NavigateTo(object parameter)
        {
            string path = parameter.ToString();
            switch (path)
            {
                case "purposes":
                    Navigation.PushAsync(new PurposesLevel1PageView());
                    break;
                case "categories":
                    Navigation.PushAsync(new CategoriesLevel1PageView());
                    break;
                case "reports":
                    Navigation.PushAsync(new ReportsLevel1PageView());
                    break;
                case "purses":
                    Navigation.PushAsync(new PursesLevel1PageView());
                    break;
                default:
                    break;
            }
        }
    }
}