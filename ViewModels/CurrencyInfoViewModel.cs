namespace CurrenciesDynamicsApp.ViewModels
{
    public class CurrencyInfoViewModel : ViewModelBase
    {
        private readonly CurrencyInfo currencyInfo;

        public CurrencyInfoViewModel(CurrencyInfo currencyInfo)
        {
            this.currencyInfo = currencyInfo;
        }
        
        public int Cur_ID
        {
            get => this.currencyInfo.Cur_ID;
        }
       
        public string Date
        {
            get => this.currencyInfo.Date.ToShortDateString();
        }
        
        public string Cur_Abbreviation
        {
            get => this.currencyInfo.Cur_Abbreviation;
            set
            {
                this.currencyInfo.Cur_Abbreviation = value;
                OnPropertyChanged(nameof(Cur_Abbreviation));
            }
        }

        public string Cur_Name
        {
            get => this.currencyInfo.Cur_Name;
        }
        
        public decimal? Cur_OfficialRate
        {
            get => this.currencyInfo.Cur_OfficialRate;
            set
            {
                this.currencyInfo.Cur_OfficialRate = value;
                OnPropertyChanged(nameof(Cur_Abbreviation));
            }
        }
    }
}
