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
    public partial class StartPageView : ContentPage
    {
        public StartPageView()
        {

            BindingContext = this;
            InitializeComponent();
        }
    }
}