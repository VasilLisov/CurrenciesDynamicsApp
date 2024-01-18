using CurrenciesDynamicsApp.Commands;
using CurrenciesDynamicsApp.Interfaces;
using CurrenciesDynamicsApp.Stores;
using CurrenciesDynamicsApp.ViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace CurrenciesDynamicsApp
{
    public class ApplicationViewModel : ViewModelBase
    {
        private CurrencyInfoViewModel selectedCurrencyInfo;
        private DateTime selectedDate = DateTime.UtcNow.Date;
        private CurrenciesInfoStore currenciesInfoStore;

        private List<string> mainCurrencies = new List<string>()
        {
            "USD",
            "EUR",
            "GBP",
            "RUB"
        };

        public ApplicationViewModel(
            IFileService fileService,
            IHttpService httpService,
            IDialogService dialogService,
            IConfiguration configuration)
        {
            this.currenciesInfoStore = new CurrenciesInfoStore(fileService, configuration);

            this.DownloadFromRemoteCommand = new DownloadFromRemoteCommand(this, fileService, httpService, dialogService, configuration);
            this.OpenFromFileCommand = new OpenFromFileCommand(currenciesInfoStore, this);
            this.SaveToFileCommand = new SaveToFileCommand(currenciesInfoStore);
        }

        public ObservableCollection<CurrencyInfoViewModel> CurrencyInfos { get; set; } = new ObservableCollection<CurrencyInfoViewModel>();
        public ObservableCollection<CurrencyInfoViewModel> MainCurrenciesInfos { get; set; } = new ObservableCollection<CurrencyInfoViewModel>();

        public CurrencyInfoViewModel SelectedCurrencyInfo
        {
            get => this.selectedCurrencyInfo;
            set
            {
                this.selectedCurrencyInfo = value;
                OnPropertyChanged(nameof(SelectedCurrencyInfo));
            }
        }

        public DateTime SelectedDate
        {
            get => this.selectedDate.Date;
            set
            {
                this.selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }

        public ICommand DownloadFromRemoteCommand { get; }
        public ICommand OpenFromFileCommand { get; }
        public ICommand SaveToFileCommand { get; }

        public void UpdateCurrenciesDisplay(List<CurrencyInfo> currencyInfos)
        {
            this.CurrencyInfos.Clear();
            this.MainCurrenciesInfos.Clear();

            foreach (CurrencyInfo item in currencyInfos)
            {
                CurrencyInfoViewModel viewModel = new CurrencyInfoViewModel(item);
                this.CurrencyInfos.Add(viewModel);
                
                if (this.mainCurrencies.Any(currency => currency.Equals(item.Cur_Abbreviation, StringComparison.InvariantCultureIgnoreCase)))
                {
                    this.MainCurrenciesInfos.Add(viewModel);
                }
            }
        }
    }
}
