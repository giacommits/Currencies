using CurrenciesLibrary.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CurrenciesLibrary.CurrenciesAPI
{
    public interface IAPIHelper
    {
        HttpClient ApiClient { get; set; }
        bool LocalApi { get; set; }

        Task<CurrenciesRateAPIModel> GetRateFromAPIAsync(string baseCurrency, string quoteCurrency, string date);
        Task<Dictionary<string, string>> GetCurrenciesListFromApiAsync();
        Task<DatesRangeApiModel> GetDatesRangeAsync();
    }
}