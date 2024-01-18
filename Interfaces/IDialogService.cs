namespace CurrenciesDynamicsApp.Interfaces
{
    public interface IDialogService
    {
        void DisplayInfo(string message);
        void DisplayWarning(string message);
        void ReportErrors(string message);
        bool AskForConfirmation(string confirmationRequest);
    }
}
