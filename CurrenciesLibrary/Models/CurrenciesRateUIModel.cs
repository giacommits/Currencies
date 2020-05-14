using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrenciesLibrary.Models
{ 
    public class CurrenciesRateUIModel  
    {
        public Dictionary<string, decimal> Rates { get; set; }  // for example USD: 1.3254
        
    }
}
