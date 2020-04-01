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
    public partial class ReportsLevel2PageView : ContentPage
    {
        public ICommand DeleteCommand { get; set; }

        public Report Report { get; set; }

        public ReportsLevel2PageView(Report report)
        {
            Report = report;
            DeleteCommand = new Command(Delete);

            BindingContext = this;
            InitializeComponent();
        }

        public void Delete()
        {
            // action here
        }
    }
}