using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrenciesLibrary.CurrenciesAPI
{
    public static class EndpointUrl
    {
        //Generates the string for the respective endpoint
        public static string GenerateString(string baseCurrency, string quoteCurrency, string date)
        {
            string url = "";

            if (date == DateTime.Now.ToString("yyyy-MM-dd"))
            {
                url = $"latest?symbols={quoteCurrency}&base={baseCurrency}";
            }
            else
            {
                url = $"{date}?symbols={quoteCurrency}&base={baseCurrency}";
            }

            return url;
       
        }
    }
}

