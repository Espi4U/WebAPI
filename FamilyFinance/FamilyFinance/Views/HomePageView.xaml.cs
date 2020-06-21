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
        public ICommand OpenAddNewIncomeOrExpensePageCommand { get; }

        public HomePageView()
        {
            var existingPages = Navigation.NavigationStack.ToList();
            foreach (var page in existingPages)
            {
                Navigation.RemovePage(page);
            }

            NavigationPage.SetHasNavigationBar(this, false);

            OpenAddNewIncomeOrExpensePageCommand = new Command(OpenAddNewIncomeOrExpensePage);

            BindingContext = this;
            InitializeComponent();
        }

        public void OpenAddNewIncomeOrExpensePage(object parameter)
        {
            Navigation.PushAsync(new AddIncomeOrExpensePageView());
        }
    }
}