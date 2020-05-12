using Caliburn.Micro;
using CurrenciesLibrary.CurrenciesAPI;
using CurrenciesLibrary.Models;
using CurrenciesLibrary.CurrenciesUtilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFCuerrenciesUI.ViewModels.Commands;
using System.Configuration;

namespace WPFCuerrenciesUI.ViewModels
{
	public class ShellViewModel : Screen
	{
				
		private string _baseValue ="1";
		private string _quoteValue;
		private string _selectedBase;
		private string _selectedQuote;		
		private IAPIHelper _apiHelper;
		private DateTime _date;
		private ICurrenciesList _cDictionary;
		private BindableCollection<string> _currenciesList = new BindableCollection<string>();

		public ShellViewModel(ICurrenciesList CDictionary , IAPIHelper apiHelper)
		{
			_cDictionary = CDictionary;
			_apiHelper = apiHelper;
			Calculate = new CalculateCommand(CallCalculator);
			CallGetRateAsync = new GetRateCommand(GetRateAsync, GetRateCanExecute);			
		}

		//To avoid calling asyn method on constructor
		protected async override void OnViewLoaded(object view)
		{
			base.OnViewLoaded(view);
			CanCallGetRate = false;
			await GetCurrenciesList();
			if (_apiHelper.LocalApi)
			{
				await GetDatesRangeAsync();
			}
			else
			{
				try
				{
					MinDate = DateTime.Parse(ConfigurationManager.AppSettings["remoteApiMinDate"]);
					MaxDate = DateTime.Now;
					Date = DateTime.Now;
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Error, could not retrieve dates range.");
				}
			}
			CanCallGetRate = true;
			await GetRateAsync();			
		}

        //Gets the list of avilables currencies
		private async Task GetCurrenciesList()
		{

			try
			{
				Dictionary<string, string> ListOfCurrencies = new Dictionary<string, string>();

				//If app.config is set to utilize local API it get the list from an endpoint in the local API.
				if (_apiHelper.LocalApi)
				{
					ListOfCurrencies = await _apiHelper.GetCurrenciesListFromApiAsync();
					
				}
				else
				{
			    //Else it get it from a json File, since the public API https://api.exchangeratesapi.io doesnt hava such endpoint.
					ListOfCurrencies = await _cDictionary.GetCurrenciesListAsync();
				}

				foreach (KeyValuePair<string, string> x in ListOfCurrencies.OrderBy(x => x.Value))
				{

					CurrenciesList.Add($"{x.Value} ({x.Key})");
					if (x.Key == "EUR")    //Sets default selected items for comboboxes
						SelectedBase = CurrenciesList.Last();

					if (x.Key == "USD")
						SelectedQuote = CurrenciesList.Last();
				}

				if (SelectedBase == null) //Sets default selected items for comboboxes in case the above are not in the CurrenciesList
					SelectedBase = CurrenciesList[0];

				if (SelectedQuote == null)
					SelectedQuote = CurrenciesList[1];

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error, la aplicacion se cerrara.");
				Application.Current.Shutdown();
			}
		}

		//Gets the avilable dates range to pick from. This information is provided by dates/range endpoint in local API
		public async Task GetDatesRangeAsync()
		{
			try
			{
				var model = await _apiHelper.GetDatesRangeAsync();
				MinDate = model.MinDate;
				MaxDate = model.MaxDate;
				CanCallGetRate = false;
				Date = model.MaxDate;
				CanCallGetRate = true;
			}

			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error, could not retrieve dates range.");
				
			}
		}


		//Gets rate from API and calls the method that will initiates calculations
		public async Task GetRateAsync()
		{
			if (SelectedBase == SelectedQuote)
			{
				BaseValue = "";
				QuoteValue = "";
			}
			else 
			{
				if (SelectedBase != "" && SelectedQuote != "" && Date.ToString() != "")
				{
					try
					{
						string baseCurrency = new string((SelectedBase.ToString().Substring(SelectedBase.Length - 4, 3).ToArray()));
						string quoteCurrency = new string((SelectedQuote.Substring(SelectedQuote.Length - 4, 3).ToArray()));
						var model = await _apiHelper.GetRateFromAPIAsync(baseCurrency, quoteCurrency, Date.ToString("yyyy-MM-dd"));
						Rate = model.Rates.First().Value;
						if (BaseValue != "" && Rate != 0)
						{
							//Default is calulate from base value, although user can calculate from quote value using the
							//QuoteValue textbox, and CallCalculator will be called with the "QuoteValue" parameter.
							CallCalculator("BaseValue");
						}
					}
					catch (Exception ex)
					{
						CanCallGetRate = false;
						BaseValue = "";
						QuoteValue = "";
						CanCallGetRate = true;
						Date = MaxDate;
						MessageBox.Show(ex.Message, "Error");
						
						return;
					}
				}
				
			}

			
	
		}

		public void CallCalculator(string hasChanged)
		{
			
			try
			{
				//Sees if user wants to calculate from base value or quote value.
				//Checks if the values in the textboxes are correct for calling the calculator method.
				//If not, it clears them.

				if ("BaseValue" == hasChanged && BaseValue != "." && BaseValue != "")
				{	
					
					QuoteValue = CurrenciesCalculator.CalculateQuoteValue(BaseValue, Rate).
							ToString(CultureInfo.GetCultureInfo("en-US"));
						
				}

				else if ("QuoteValue" == hasChanged && QuoteValue != "." && QuoteValue != "")
				{
					
					BaseValue = CurrenciesCalculator.CalculateBaseValue(QuoteValue, Rate).
							  ToString(CultureInfo.GetCultureInfo("en-US"));
					
				}

				else
				{
					BaseValue = "";
					QuoteValue = "";
				}
			}
			catch (OverflowException)
			{
				MessageBox.Show("The input value is out of the accepted range.", "Error");
				BaseValue="";
				QuoteValue = "";
			}
			catch (FormatException)
			{
				MessageBox.Show($"The input value is invalid. Use only digits or point  \".\" as decimal separator. " 
					+ $"For example: 15.34 ", "Error");
				BaseValue = "";
				QuoteValue = "";
			}
			catch (DivideByZeroException)
			{
				MessageBox.Show("Couldn't retrieve the requested results (rate = 0).", "Error");
			}
			catch (Exception)
			{
				MessageBox.Show("Couldn't retrieve the requested results.", "Error");
			}

		}


		public CalculateCommand Calculate { get; private set; }

		public GetRateCommand CallGetRateAsync { get; private set; }

		public bool CanCallGetRate { get; set; } = true;
		public bool GetRateCanExecute()
		{
			return CanCallGetRate;
		}


		private DateTime _minDate;

		public DateTime MinDate
		{	
			get { return _minDate; }
			set 
			{ 
				_minDate = value;
				NotifyOfPropertyChange(()=> MinDate);
			}
		}

		private DateTime _maxDate;

		public DateTime MaxDate
		{
			get { return _maxDate; }
			set 
			{
				_maxDate = value;
				NotifyOfPropertyChange(() => MaxDate);
			}
		}


		public decimal Rate { get; set; }

		public BindableCollection<string> CurrenciesList
		{

			get { return _currenciesList; }

			set { _currenciesList = value; }
		}

		public string SelectedBase
		{

			get { return _selectedBase; }
			set 
			{ 
				_selectedBase = value;

				NotifyOfPropertyChange(() => SelectedBase);	
			}

		}	

		public string SelectedQuote
		{

			get { return _selectedQuote; }
			set 
			{ 
				_selectedQuote = value;

				NotifyOfPropertyChange(() => SelectedQuote);
			}

		}

		public string BaseValue
		{

			get { return _baseValue; }
			set 
			{
				_baseValue = value;

				NotifyOfPropertyChange(() => BaseValue);
			}

		}

		public string QuoteValue
		{

			get { return _quoteValue; }
			set	
			{
				_quoteValue = value;

				NotifyOfPropertyChange(() => QuoteValue);		
			}

		}
		
		public DateTime Date
		{

			get { return _date; }
			set 
			{ 
				_date = value;

				NotifyOfPropertyChange(() => Date);
			}

		}

	}
}
