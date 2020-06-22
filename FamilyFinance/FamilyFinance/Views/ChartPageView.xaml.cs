using Acr.UserDialogs;
using FamilyFinance.Helpers;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Entry = Microcharts.Entry;

namespace FamilyFinance.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChartPageView : ContentPage
    {
        private readonly APIClient _apiClient;

        private Random _random = new Random();

        private Dictionary<string, int> _incomes;
        public Dictionary<string, int> Incomes
        {
            get => _incomes;
            set
            {
                _incomes = value;
                OnPropertyChanged(nameof(Incomes));
            }
        }

        private Dictionary<string, int> _expenses;
        public Dictionary<string, int> Expenses
        {
            get => _expenses;
            set
            {
                _expenses = value;
                OnPropertyChanged(nameof(Expenses));
            }
        }

        private List<Entry> _entriesIncomes;
        public List<Entry> EntriesIncomes
        {
            get => _entriesIncomes;
            set
            {
                _entriesIncomes = value;
                OnPropertyChanged(nameof(EntriesIncomes));
            }
        }

        private List<Entry> _entriesExpenses;
        public List<Entry> EntriesExpenses
        {
            get => _entriesExpenses;
            set
            {
                _entriesExpenses = value;
                OnPropertyChanged(nameof(EntriesExpenses));
            }
        }

        public ChartPageView()
        {
            _apiClient = new APIClient();

            EntriesIncomes = new List<Entry>();
            EntriesExpenses = new List<Entry>();

            BindingContext = this;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            LoadDataToCharts();
        }

        private async void LoadDataToCharts()
        {
            var response = await _apiClient.LoadDataByCategoryToChart(GlobalHelper.GetBaseRequest());
            if (!response.IsSuccess || !response.BaseIsSuccess)
            {
                return;
            }

            Incomes = response.Incomes;
            Expenses = response.Expenses;

            GenerateCharts();
        }

        private void GenerateCharts()
        {
            UserDialogs.Instance.ShowLoading();
            foreach (KeyValuePair<string, int> keyValue in Incomes)
            {
                EntriesIncomes.Add(new Entry(keyValue.Value) { Color = GetRandomeColor(), Label = keyValue.Key, ValueLabel = keyValue.Value.ToString(), TextColor = GetRandomeColor() });
            }
            foreach (KeyValuePair<string, int> keyValue in Expenses)
            {
                EntriesExpenses.Add(new Entry(keyValue.Value) { Color = GetRandomeColor(), Label = keyValue.Key, ValueLabel = keyValue.Value.ToString(), TextColor = GetRandomeColor() });
            }

            IncomesComponent.Chart = new DonutChart() { Entries = EntriesIncomes };
            ExpensesComponent.Chart = new DonutChart() { Entries = EntriesExpenses };

            UserDialogs.Instance.HideLoading();
        }

        private SKColor GetRandomeColor()
        {
            return new SKColor((byte)_random.Next(150), (byte)_random.Next(150), (byte)_random.Next(150));
        }
    }
}