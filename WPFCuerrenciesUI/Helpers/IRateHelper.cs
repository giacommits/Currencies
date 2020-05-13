using System;
using System.Threading.Tasks;

namespace WPFCuerrenciesUI.Helpers
{
    public interface IRateHelper
    {
        Task<decimal> GetRateAsync(string selectedBase, string selectedQuote, DateTime date);
    }
}