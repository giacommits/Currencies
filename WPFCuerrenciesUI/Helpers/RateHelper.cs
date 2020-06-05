using CurrenciesLibrary.CurrenciesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCuerrenciesUI.Helpers
{
    public class RateHelper : IRateHelper
    {
        IAPIHelper _apiHelper;
        public RateHelper(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async  Task<decimal> GetRateAsync(string selectedBase, string selectedQuote, DateTime date)
        {
            string baseCurrency;
            string quoteCurrency;
            try
            {
                baseCurrency = new string((selectedBase.ToString().Substring(selectedBase.Length - 4, 3).ToArray()));
                quoteCurrency = new string((selectedQuote.Substring(selectedQuote.Length - 4, 3).ToArray()));
            }
            catch
            {
                throw new Exception("Invalid currencies names");
            }
            var model = await _apiHelper.GetRateFromAPIAsync(baseCurrency, quoteCurrency, date.ToString("yyyy-MM-dd"));
                return model.Rates.First().Value;
        }
    }
}
