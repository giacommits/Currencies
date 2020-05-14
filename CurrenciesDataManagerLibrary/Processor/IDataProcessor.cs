using CurrenciesDataManagerLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrenciesDataManagerLibrary.Processor
{
    public interface IDataProcessor
    {
        Task<CurrenciesRateApiModel> GetRateAsync(string baseCurrency, string quoteCurrency, string date);
        Task<Dictionary<string, string>> GetCurrenciesListAsync();
        Task<DatesRangeApiModel> GetDatesRangeAsync();
    }
}