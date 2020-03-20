using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FamilyFinance.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PurposesLevel2PageView : ContentPage
    {
        public string PageTitle { get; set; }

        public PurposesLevel2PageView(string pageTitle)
        {
            PageTitle = pageTitle;

            BindingContext = this;
            InitializeComponent();
        }
    }
}