using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrenciesDataManagerLibrary.Models
{
    public class CurrenciesListApiModel
    {
		private Dictionary<string,string> _currenciesList;

		public Dictionary<string,string> CurrenciesList
		{
			get { return _currenciesList; }
			set
			{
				_currenciesList = new Dictionary<string, string>();
				_currenciesList = value; 
			}
		}

	}
}
