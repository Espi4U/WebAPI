﻿using FamilyFinance.Helpers;
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
        private APIClient _apiClient;

        private List<Purpose> _purposes;
        public List<Purpose> Purposes
        {
            get => _purposes;
            set
            {
                _purposes = value;
                OnPropertyChanged(nameof(Purposes));
            }
        }

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
            _apiClient = new APIClient();

            BindingContext = this;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            LoadPurposesAsync();
        }

        private async void LoadPurposesAsync()
        {
            var response = await _apiClient.GetPurposesAsync(GlobalHelper.GetBaseRequest());
            if(!response.BaseIsSuccess || !response.IsSuccess)
            {
                return;
            }

            Purposes = response.Purposes;
        }
    }
}