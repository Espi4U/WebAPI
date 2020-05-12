using System;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace FamilyFinance.Views
{
    [DesignTimeVisible(false)]
    public partial class MainPageView : Xamarin.Forms.TabbedPage
    {
        public MainPageView()
        {
            var existingPages = Navigation.NavigationStack.ToList();
            foreach (var page in existingPages)
            {
                Navigation.RemovePage(page);
            }

            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
        }
    }
}