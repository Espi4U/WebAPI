using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Models.APIModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FamilyFinance.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportsLevel1PageView : ContentPage
    {

        public ObservableCollection<Report> Reports { get; set; }

        public Report SelectedReport
        {
            get => null;
            set
            {
                if (value != null)
                {
                    OnPropertyChanged(nameof(SelectedReport));
                    Navigation.PushAsync(new ReportsLevel2PageView(value));
                }
            }
        }
        public ReportsLevel1PageView()
        {
            Reports = new ObservableCollection<Report>
            {
                new Report(){ Name="Report 1", Text="blablabla", Date=DateTime.Now},
                new Report(){ Name="Report 2", Text="blablabla", Date=DateTime.Now},
                new Report(){ Name="Report 3", Text="blablabla", Date=DateTime.Now},
                new Report(){ Name="Report 4", Text="blablabla", Date=DateTime.Now},
                new Report(){ Name="Report 5", Text="blablabla", Date=DateTime.Now},
                new Report(){ Name="Report 6", Text="blablabla", Date=DateTime.Now},
                new Report(){ Name="Report 7", Text="blablabla", Date=DateTime.Now},
            };

            BindingContext = this;
            InitializeComponent();
        }
    }
}