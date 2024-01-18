using CurrenciesDynamicsApp.Stores;

namespace CurrenciesDynamicsApp.Commands
{
    public class OpenFromFileCommand : CommandBase
    {
        private readonly CurrenciesInfoStore currenciesInfoStore;
        private readonly ApplicationViewModel viewModel;

        public OpenFromFileCommand(
            CurrenciesInfoStore infoStore,
            ApplicationViewModel viewModel)
        {
            this.currenciesInfoStore = infoStore;
            this.viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            this.currenciesInfoStore.LoadFromFile();
            this.viewModel.UpdateCurrenciesDisplay(this.currenciesInfoStore.CurrencyInfos);
        }
    }
}
