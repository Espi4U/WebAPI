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
    public partial class HomePageView : ContentPage
    {
        public ICommand AddCommand { get; }

        public HomePageView()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            AddCommand = new Command(GoToAddNewIncomeOrExpensePage);

            BindingContext = this;
            InitializeComponent();
        }

        public void GoToAddNewIncomeOrExpensePage(object parameter)
        {
            Navigation.PushAsync(new AddIncomeOrExpensePageView());
        }
    }
}