using CurrenciesLibrary.CurrenciesAPI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MVCCurrenciesUI.Controllers.Helpers
{
    public class RateHelper : IRateHelper
    {
        IAPIHelper _apiHelper;
        public RateHelper(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<string> GetRateAsync(string selectedBase, string selectedQuote, DateTime date)
        {
            if (selectedBase == selectedQuote)
            {
                return "1";
            }
            else
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
                var info = new NumberFormatInfo();
                info.NumberDecimalSeparator = ".";

                return (model.Rates.First().Value).ToString(info);
            }
        }
    }
}