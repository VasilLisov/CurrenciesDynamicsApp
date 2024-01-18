using System.Collections.Generic;

namespace CurrenciesDynamicsApp.Interfaces
{
    public interface IFileService
    {
        List<CurrencyInfo> Open(string fileName);
        void Save(string fileName, List<CurrencyInfo> currencyInfoList);
    }
}
