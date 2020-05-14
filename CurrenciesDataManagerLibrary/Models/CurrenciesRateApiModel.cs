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
            rates = new Dictionary<string, decimal>();
        }
        public Dictionary<string,decimal> rates { get; set; }
        public string Base { get; set; }
        public string Date { get; set; }
    }

   

}
