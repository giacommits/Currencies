using CurrenciesLibrary.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CurrenciesLibrary.CurrenciesAPI
{
    public interface IAPIHelper
    {
        HttpClient ApiClient { get; set; }
        bool InternalApi { get; set; }

        Task<CurrenciesRateUIModel> GetRateFromAPIAsync(string baseCurrency, string quoteCurrency, string date);
        Task<Dictionary<string, string>> GetCurrenciesListFromApiAsync();
        Task<DatesRangeUIModel> GetDatesRangeAsync();
    }
}