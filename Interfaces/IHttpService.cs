using System;
using System.Threading.Tasks;

namespace CurrenciesDynamicsApp.Interfaces
{
    public interface IHttpService
    {
        Task<string> GetAsync(DateTime onDate);
    }
}
