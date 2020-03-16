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
        public ICommand AddNewIncomeOrExpenseCommand { get; }

        public HomePageView()
        {
            BindingContext = this;

            AddNewIncomeOrExpenseCommand = new Command(AddNewIncomeOrExpense);

            InitializeComponent();
        }

        public void AddNewIncomeOrExpense(object parameter)
        {

        }
    }
}