using Caliburn.Micro;
using CurrenciesLibrary.CurrenciesAPI;
using CurrenciesLibrary.CurrenciesUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCuerrenciesUI.Helpers
{
	public class CurrenciesListHelper : ICurrenciesListHelper
	{
		private IAPIHelper _apiHelper;
		private ICurrenciesList _currenciesListFromFile;
		

		public CurrenciesListHelper(IAPIHelper apiHelper, ICurrenciesList currenciesListFromFile)
		{
			_apiHelper = apiHelper;
			_currenciesListFromFile = currenciesListFromFile;
		}


		public async Task<BindableCollection<string>> GetCurrenciesListAsync()
		{

			BindableCollection<string> currenciesList = new BindableCollection<string>();
			Dictionary<string, string> currenciesDictionary = new Dictionary<string, string>();

			//If app.config is set to utilize internal API it get the list of avilables currencies from one of its endpoints.
			if (_apiHelper.InternalApi)
			{
				currenciesDictionary = await _apiHelper.GetCurrenciesListFromApiAsync();
			}

			else
			{
				//Else it get it from a json File, since the public API https://api.exchangeratesapi.io doesnt hava such endpoint.
				currenciesDictionary = await _currenciesListFromFile.GetCurrenciesListAsync();
			}

			foreach (KeyValuePair<string, string> x in currenciesDictionary.OrderBy(x => x.Value))
			{
				currenciesList.Add($"{x.Value} ({x.Key})");
			}

			return currenciesList;
		}

	}
}
