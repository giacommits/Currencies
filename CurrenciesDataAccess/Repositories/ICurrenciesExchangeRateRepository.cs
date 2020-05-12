using CurrenciesDataAccess.Models;
using System;
using System.Threading.Tasks;

namespace CurrenciesDataAccess.Repositories
{
    public interface ICurrenciesExchangeRateRepository : IDisposable
    {
        Task<CurrenciesExchangeRate> GetRateAsync(string baseCurrency, string quoteCurrency, string date);
    }
}