using CurrenciesDynamicsApp.Stores;

namespace CurrenciesDynamicsApp.Commands
{
    public class SaveToFileCommand : CommandBase
    {
        private readonly CurrenciesInfoStore currenciesInfoStore;

        public SaveToFileCommand(
            CurrenciesInfoStore currenciesInfoStore)
        {
            this.currenciesInfoStore = currenciesInfoStore;
        }

        public override void Execute(object parameter)
        {
            this.currenciesInfoStore.SaveToFile();
        }
    }
}
