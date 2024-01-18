using CurrenciesDynamicsApp.Interfaces;
using System.Windows;

namespace CurrenciesDynamicsApp.Services
{
    public class DialogService : IDialogService
    {
        public bool AskForConfirmation(string confirmationRequest)
        {
            return
                MessageBox.Show(confirmationRequest, "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK;

        }

        public void DisplayInfo(string message)
        {
            MessageBox.Show(message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void DisplayWarning(string message)
        {
            MessageBox.Show(message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public void ReportErrors(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

    }
}
