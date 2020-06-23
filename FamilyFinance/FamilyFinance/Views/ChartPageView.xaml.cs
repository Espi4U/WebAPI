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

        private Dictionary<string, int> _incomesUAH;
        public Dictionary<string, int> IncomesUAH
        {
            get => _incomesUAH;
            set
            {
                _incomesUAH = value;
                OnPropertyChanged(nameof(IncomesUAH));
            }
        }

        private Dictionary<string, int> _expensesUAH;
        public Dictionary<string, int> ExpensesUAH
        {
            get => _expensesUAH;
            set
            {
                _expensesUAH = value;
                OnPropertyChanged(nameof(ExpensesUAH));
            }
        }

        private Dictionary<string, int> _incomesUSD;
        public Dictionary<string, int> IncomesUSD
        {
            get => _incomesUSD;
            set
            {
                _incomesUSD = value;
                OnPropertyChanged(nameof(IncomesUSD));
            }
        }

        private Dictionary<string, int> _expensesUSD;
        public Dictionary<string, int> ExpensesUSD
        {
            get => _expensesUSD;
            set
            {
                _expensesUSD = value;
                OnPropertyChanged(nameof(ExpensesUSD));
            }
        }

        private Dictionary<string, int> _incomesEUR;
        public Dictionary<string, int> IncomesEUR
        {
            get => _incomesEUR;
            set
            {
                _incomesEUR = value;
                OnPropertyChanged(nameof(IncomesEUR));
            }
        }

        private Dictionary<string, int> _expensesEUR;
        public Dictionary<string, int> ExpensesEUR
        {
            get => _expensesEUR;
            set
            {
                _expensesEUR = value;
                OnPropertyChanged(nameof(ExpensesEUR));
            }
        }

        private Dictionary<string, int> _incomesPLZ;
        public Dictionary<string, int> IncomesPLZ
        {
            get => _incomesPLZ;
            set
            {
                _incomesPLZ = value;
                OnPropertyChanged(nameof(IncomesPLZ));
            }
        }

        private Dictionary<string, int> _expensesPLZ;
        public Dictionary<string, int> ExpensesPLZ
        {
            get => _expensesPLZ;
            set
            {
                _expensesPLZ = value;
                OnPropertyChanged(nameof(ExpensesPLZ));
            }
        }

        private List<Entry> _entriesIncomesUAH;
        public List<Entry> EntriesIncomesUAH
        {
            get => _entriesIncomesUAH;
            set
            {
                _entriesIncomesUAH = value;
                OnPropertyChanged(nameof(EntriesIncomesUAH));
            }
        }

        private List<Entry> _entriesExpensesUAH;
        public List<Entry> EntriesExpensesUAH
        {
            get => _entriesExpensesUAH;
            set
            {
                _entriesExpensesUAH = value;
                OnPropertyChanged(nameof(EntriesExpensesUAH));
            }
        }

        private List<Entry> _entriesIncomesUSD;
        public List<Entry> EntriesIncomesUSD
        {
            get => _entriesIncomesUSD;
            set
            {
                _entriesIncomesUSD = value;
                OnPropertyChanged(nameof(EntriesIncomesUSD));
            }
        }

        private List<Entry> _entriesExpensesUSD;
        public List<Entry> EntriesExpensesUSD
        {
            get => _entriesExpensesUSD;
            set
            {
                _entriesExpensesUSD = value;
                OnPropertyChanged(nameof(EntriesExpensesUSD));
            }
        }

        private List<Entry> _entriesIncomesEUR;
        public List<Entry> EntriesIncomesEUR
        {
            get => _entriesIncomesEUR;
            set
            {
                _entriesIncomesEUR = value;
                OnPropertyChanged(nameof(EntriesIncomesEUR));
            }
        }

        private List<Entry> _entriesExpensesEUR;
        public List<Entry> EntriesExpensesEUR
        {
            get => _entriesExpensesEUR;
            set
            {
                _entriesExpensesEUR = value;
                OnPropertyChanged(nameof(EntriesExpensesEUR));
            }
        }

        private List<Entry> _entriesIncomesPLZ;
        public List<Entry> EntriesIncomesPLZ
        {
            get => _entriesIncomesPLZ;
            set
            {
                _entriesIncomesPLZ = value;
                OnPropertyChanged(nameof(EntriesIncomesPLZ));
            }
        }

        private List<Entry> _entriesExpensesPLZ;
        public List<Entry> EntriesExpensesPLZ
        {
            get => _entriesExpensesPLZ;
            set
            {
                _entriesExpensesPLZ = value;
                OnPropertyChanged(nameof(EntriesExpensesPLZ));
            }
        }

        public ChartPageView()
        {
            _apiClient = new APIClient();

            EntriesIncomesUAH = new List<Entry>();
            EntriesExpensesUAH = new List<Entry>();

            EntriesIncomesUSD = new List<Entry>();
            EntriesExpensesUSD = new List<Entry>();

            EntriesIncomesEUR = new List<Entry>();
            EntriesExpensesEUR = new List<Entry>();

            EntriesIncomesPLZ = new List<Entry>();
            EntriesExpensesPLZ = new List<Entry>();


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

            IncomesUAH = response.IncomesUAH;
            ExpensesUAH = response.ExpensesUAH;

            IncomesUSD = response.IncomesUSD;
            ExpensesUSD = response.ExpensesUSD;

            IncomesEUR = response.IncomesEUR;
            ExpensesEUR = response.ExpensesEUR;

            IncomesPLZ = response.IncomesPLZ;
            ExpensesPLZ = response.ExpensesPLZ;

            GenerateCharts();
        }

        private void GenerateCharts()
        {
            UserDialogs.Instance.ShowLoading();
            foreach (KeyValuePair<string, int> keyValue in IncomesUAH)
            {
                EntriesIncomesUAH.Add(new Entry(keyValue.Value) { Color = GetRandomeColor(), Label = keyValue.Key, ValueLabel = keyValue.Value.ToString(), TextColor = GetRandomeColor() });
            }
            foreach (KeyValuePair<string, int> keyValue in ExpensesUAH)
            {
                EntriesExpensesUAH.Add(new Entry(keyValue.Value) { Color = GetRandomeColor(), Label = keyValue.Key, ValueLabel = keyValue.Value.ToString(), TextColor = GetRandomeColor() });
            }
            foreach (KeyValuePair<string, int> keyValue in IncomesUSD)
            {
                EntriesIncomesUSD.Add(new Entry(keyValue.Value) { Color = GetRandomeColor(), Label = keyValue.Key, ValueLabel = keyValue.Value.ToString(), TextColor = GetRandomeColor() });
            }
            foreach (KeyValuePair<string, int> keyValue in ExpensesUSD)
            {
                EntriesExpensesUSD.Add(new Entry(keyValue.Value) { Color = GetRandomeColor(), Label = keyValue.Key, ValueLabel = keyValue.Value.ToString(), TextColor = GetRandomeColor() });
            }
            foreach (KeyValuePair<string, int> keyValue in IncomesEUR)
            {
                EntriesIncomesEUR.Add(new Entry(keyValue.Value) { Color = GetRandomeColor(), Label = keyValue.Key, ValueLabel = keyValue.Value.ToString(), TextColor = GetRandomeColor() });
            }
            foreach (KeyValuePair<string, int> keyValue in ExpensesEUR)
            {
                EntriesExpensesEUR.Add(new Entry(keyValue.Value) { Color = GetRandomeColor(), Label = keyValue.Key, ValueLabel = keyValue.Value.ToString(), TextColor = GetRandomeColor() });
            }
            foreach (KeyValuePair<string, int> keyValue in IncomesPLZ)
            {
                EntriesIncomesPLZ.Add(new Entry(keyValue.Value) { Color = GetRandomeColor(), Label = keyValue.Key, ValueLabel = keyValue.Value.ToString(), TextColor = GetRandomeColor() });
            }
            foreach (KeyValuePair<string, int> keyValue in ExpensesPLZ)
            {
                EntriesExpensesPLZ.Add(new Entry(keyValue.Value) { Color = GetRandomeColor(), Label = keyValue.Key, ValueLabel = keyValue.Value.ToString(), TextColor = GetRandomeColor() });
            }

            IncomesComponentUAH.Chart = new DonutChart() { Entries = EntriesIncomesUAH };
            ExpensesComponentUAH.Chart = new DonutChart() { Entries = EntriesExpensesUAH };

            IncomesComponentUSD.Chart = new DonutChart() { Entries = EntriesIncomesUSD };
            ExpensesComponentUSD.Chart = new DonutChart() { Entries = EntriesExpensesUSD };

            IncomesComponentEUR.Chart = new DonutChart() { Entries = EntriesIncomesEUR };
            ExpensesComponentEUR.Chart = new DonutChart() { Entries = EntriesExpensesEUR };

            IncomesComponentPLZ.Chart = new DonutChart() { Entries = EntriesIncomesPLZ };
            ExpensesComponentPLZ.Chart = new DonutChart() { Entries = EntriesExpensesPLZ };

            UserDialogs.Instance.HideLoading();
        }

        private SKColor GetRandomeColor()
        {
            return new SKColor((byte)_random.Next(150), (byte)_random.Next(150), (byte)_random.Next(150));
        }
    }
}