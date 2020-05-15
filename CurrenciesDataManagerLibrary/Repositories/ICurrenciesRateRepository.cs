using System;
using System.Threading.Tasks;

namespace CurrenciesDataManagerLibrary.Repositories
{
    public interface ICurrenciesRateRepository : IDisposable
    {
        Task<decimal> GetRateAsync(string baseCurrency, string quoteCurrency, string date);
    }
}