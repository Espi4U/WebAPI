using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FamilyFinance.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public ICommand GetStatsCommand { get; set; }
        public ICommand AddIncomeCommand { get; set; }

        public MainPageViewModel()
        {
            GetStatsCommand = new Command(GetStats);
            AddIncomeCommand = new Command(AddIncome);
        }

        private void GetStats()
        {

        }

        private void AddIncome()
        {

        }
    }
}
