using CurrenciesDynamicsApp.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace CurrenciesDynamicsApp.Commands
{
    public class DownloadFromRemoteCommand : AsyncCommandBase
    {
        private readonly ApplicationViewModel viewModel;
        private readonly IHttpService httpService;
        private readonly IFileService fileService;
        private readonly IDialogService dialogService;
        private readonly IConfiguration configuration;

        public DownloadFromRemoteCommand(
            ApplicationViewModel viewModel,
            IFileService fileService,
            IHttpService httpService,
            IDialogService dialogService,
            IConfiguration configuration)
        {
            this.fileService = fileService;
            this.httpService = httpService;
            this.dialogService = dialogService;
            this.configuration = configuration;
            this.viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            bool confirmed = this.dialogService.AskForConfirmation("Download currency rates from nbrb.by ?");

            if (!confirmed)
            {
                return;
            }

            TruncateLocalFile();
            List<CurrencyInfo> rates = await ProcessRequest();
            WriteToLocalFile(rates);
            this.dialogService.DisplayInfo("Currency rates downloaded");
        }

        private void TruncateLocalFile()
        {
            string filePath = this.configuration.GetValue<string>("downloadFilePath");

            using (FileStream fs = new FileStream(filePath, FileMode.Truncate))
            {
            }
        }

        private async Task<List<CurrencyInfo>> ProcessRequest()
        {
            List<CurrencyInfo> rates;

            string json = await this.httpService.GetAsync(this.viewModel.SelectedDate);

            using (MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                DataContractJsonSerializerSettings settings = new DataContractJsonSerializerSettings();
                settings.DateTimeFormat = new System.Runtime.Serialization.DateTimeFormat("yyyy-MM-ddTHH:mm:ss");
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<CurrencyInfo>), settings);
                rates = (List<CurrencyInfo>)serializer.ReadObject(ms);
            }

            return rates;
        }

        private void WriteToLocalFile(List<CurrencyInfo> rates)
        {
            string filePath = this.configuration.GetValue<string>("downloadFilePath");
            this.fileService.Save(filePath, rates);
        }
    }
}
