using System;
using System.Threading.Tasks;

namespace MVCCurrenciesUI.Controllers.Helpers
{
    public interface IRateHelper
    {
        Task<string> GetRateAsync(string selectedBase, string selectedQuote, DateTime date);
    }
}