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
            if (selectedBase != selectedQuote)
            {
                string baseCurrency = new string((selectedBase.ToString().Substring(selectedBase.Length - 4, 3).ToArray()));
                string quoteCurrency = new string((selectedQuote.Substring(selectedQuote.Length - 4, 3).ToArray()));
                var model = await _apiHelper.GetRateFromAPIAsync(baseCurrency, quoteCurrency, date.ToString("yyyy-MM-dd"));
                return model.Rates.First().Value;
            }
            else
            {
                return 0;
            }
        }
    }
}
