﻿using FamilyFinance.Helpers;
using Shared.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FamilyFinance.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationNewWithKeyPageView : ContentPage
    {
        private APIClient _apiClient;

        private RegistrationRequest _model;
        public RegistrationRequest Model
        {
            get => _model;
            set
            {
                _model = value;
                OnPropertyChanged(nameof(Model));
            }
        }

        public ICommand RegistrationCommand { get; }

        public RegistrationNewWithKeyPageView()
        {
            _apiClient = new APIClient();

            RegistrationCommand = new Command(RegistrationAsync);

            BindingContext = this;
            InitializeComponent();
        }

        private async void RegistrationAsync()
        {
            var response = await _apiClient.RegistrationNewAsync(Model);
            if (!response.BaseIsSuccess || !response.IsSuccess)
            {
                AlertHelper.ShowAlertMessage(response, this);
                return;
            }

            Navigation.PushAsync(new LoginPageView());
        }
    }
}