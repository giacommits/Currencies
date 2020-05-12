using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrenciesLibrary.CurrenciesUtilities

{
    public static class CurrenciesCalculator  
    {
        //Given base value and rate it calculates the quote value.
        public static decimal CalculateQuoteValue(string baseValue, decimal rate)
        {
             return Math.Round(decimal.Parse(baseValue, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture) * rate, 4);
        }

        //Given the quote value and rate it calculates the base value. 
        public static decimal CalculateBaseValue(string quoteValue, decimal rate)
        {                       
             return Math.Round(decimal.Parse(quoteValue, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture) / rate, 4);           
        }
    }

    
}
