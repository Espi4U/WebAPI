using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Models.APIModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FamilyFinance.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PurposesLevel1PageView : ContentPage
    {
        public List<Purpose> Purposes { get; set; }

        public PurposesLevel1PageView()
        {
            BindingContext = this;
            InitializeComponent();
        }

        private void GetAllMyPurposes()
        {
            //call api here
        }

        private void GetAllFamilyPurposes()
        {
            //call api here
        }
    }
}