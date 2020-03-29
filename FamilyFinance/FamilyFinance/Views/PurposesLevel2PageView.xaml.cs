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
    public partial class PurposesLevel2PageView : ContentPage
    {
        public Purpose Purpose { get; set; }

        public ICommand EnlargeCommand { get; }
        public ICommand DeleteCommand { get; }

        public PurposesLevel2PageView(Purpose purpose)
        {
            Purpose = purpose;

            EnlargeCommand = new Command(Enlarge);
            DeleteCommand = new Command(Delete);

            BindingContext = this;
            InitializeComponent();
        }

        private void Enlarge()
        {
            //goto edit page
        }

        private void Delete()
        {
            //call API for delete this purpose
        }
    }
}