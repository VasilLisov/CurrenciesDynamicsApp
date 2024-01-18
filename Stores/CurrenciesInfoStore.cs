using CurrenciesDynamicsApp.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace CurrenciesDynamicsApp.Stores
{
    public class CurrenciesInfoStore
    {
        private List<CurrencyInfo> currencyInfos = new List<CurrencyInfo>();
        private readonly IFileService fileService;
        private readonly IConfiguration configuration;

        public CurrenciesInfoStore(
            IFileService fileService,
            IConfiguration configuration)
        {
            this.fileService = fileService;
            this.configuration = configuration;
        }

        public List<CurrencyInfo> CurrencyInfos
        {
            get => currencyInfos;
            set => currencyInfos = value;
        }

        public void LoadFromFile()
        {
            string filePath = this.configuration.GetValue<string>("downloadFilePath");
            this.CurrencyInfos = this.fileService.Open(filePath);
        }

        public void SaveToFile()
        {
            string filePath = this.configuration.GetValue<string>("downloadFilePath");
            this.fileService.Save(filePath, this.currencyInfos);
        }
    }
}
