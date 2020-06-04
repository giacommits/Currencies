using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrenciesDataManagerLibrary.Models
{

    public class CurrenciesRateApiModel
    {
        public CurrenciesRateApiModel()
        {
            Rates = new Dictionary<string, decimal>();
        }
        public Dictionary<string,decimal> Rates { get; set; }
        public string Base { get; set; }
        public string Date { get; set; }
    }

   

}
