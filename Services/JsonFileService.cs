using CurrenciesDynamicsApp.Interfaces;
using CurrenciesDynamicsApp.Stores;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;

namespace CurrenciesDynamicsApp.Services
{
    public class JsonFileService : IFileService
    {
        private readonly IDialogService dialogService;

        public JsonFileService(IDialogService dialogService)
        {
            this.dialogService = dialogService;
        }

        public List<CurrencyInfo> Open(string fileName)
        {
            DataContractJsonSerializer jsonFormatter =
                new DataContractJsonSerializer(typeof(List<CurrencyInfo>));

            using (FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                List<CurrencyInfo> currencies = new List<CurrencyInfo>();

                try
                {
                    currencies = jsonFormatter.ReadObject(fileStream) as List<CurrencyInfo>;
                    bool fileNotEmpty = currencies.Count > 0;

                    if (fileNotEmpty)
                    {
                        this.dialogService.DisplayInfo("Reading file completed");
                    }
                    else
                    {
                        this.dialogService.DisplayInfo("File is empty");
                    }

                    return currencies;

                }
                catch
                {
                    this.dialogService.ReportErrors("Failed to read file");
                }

                return currencies;
            }
        }

        public void Save(string fileName, List<CurrencyInfo> currencyInfoList)
        {
            DataContractJsonSerializer jsonFormatter =
                new DataContractJsonSerializer(typeof(List<CurrencyInfo>));

            using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
            {
                try
                {
                    jsonFormatter.WriteObject(fileStream, currencyInfoList);
                    this.dialogService.DisplayInfo("Saved to the file");
                }
                catch
                {
                    this.dialogService.ReportErrors("Failed to write to the file");
                }
            }
        }
    }
}
