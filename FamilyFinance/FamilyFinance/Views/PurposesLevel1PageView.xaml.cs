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
    public partial class PurposesLevel1PageView : ContentPage
    {
        public ObservableCollection<Purpose> Purposes { get; set; }

        public Purpose SelectedPurpose
        {
            get => null;
            set
            {
                if(value != null)
                {
                    OnPropertyChanged(nameof(SelectedPurpose));
                    Navigation.PushAsync(new PurposesLevel2PageView(value));
                }
            }
        }

        public PurposesLevel1PageView()
        {
            Purposes = new ObservableCollection<Purpose>
            {
                new Purpose(){Name="Purpose 1", FinalSize=4, CurrentSize=2},
                new Purpose(){Name="Purpose 2", FinalSize=6, CurrentSize=6},
                new Purpose(){Name="Purpose 3", FinalSize=4, CurrentSize=0},
                new Purpose(){Name="Purpose 4", FinalSize=4, CurrentSize=2},
                new Purpose(){Name="Purpose 5", FinalSize=20, CurrentSize=2},
                new Purpose(){Name="Purpose 6", FinalSize=1, CurrentSize=1},
                new Purpose(){Name="Purpose 7", FinalSize=10, CurrentSize=2},
                new Purpose(){Name="Purpose 8", FinalSize=4, CurrentSize=2},
                new Purpose(){Name="Purpose 9", FinalSize=21, CurrentSize=3}
            };
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