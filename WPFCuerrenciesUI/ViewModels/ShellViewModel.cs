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
using WPFCuerrenciesUI.Helpers;

namespace WPFCuerrenciesUI.ViewModels
{
	public class ShellViewModel : Screen
	{
				
		private string _baseValue ="1";
		private string _quoteValue;
		private string _selectedBase;
		private string _selectedQuote;		
		private IDatesRangeHelper _datesRangeHelper;
		private ICurrenciesListHelper _currenciesListHelper;
		private IRateHelper _rateHelper;
		private DateTime _date;		
		private BindableCollection<string> _currenciesList = new BindableCollection<string>();

		public ShellViewModel( IDatesRangeHelper dateRangeHelper,
			ICurrenciesListHelper currenciesListHelper,	IRateHelper rateHelper)
		{			
			_datesRangeHelper = dateRangeHelper;
			_currenciesListHelper = currenciesListHelper;
			_rateHelper = rateHelper;
			Calculate = new CalculateCommand(Calculator);
			CallGetRateAsync = new GetRateCommand(GetRateAsync, GetRateCanExecute);			
		}

		//To avoid calling async method on constructor
		protected async override void OnViewLoaded(object view)
		{
			base.OnViewLoaded(view);

			//Changes in controls automatically calls the API for updating values, setting this property to false avoids it
			CanCallGetRate = false;

			//Gets the list of avilables currencies
			try
			{
				CurrenciesList = await _currenciesListHelper.GetCurrenciesListAsync();
				//Sets initial values for comboboxes
				SetSelectedCurrencies();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error, could not retrive currencies list. Sorry, shutting down... ");
				Application.Current.Shutdown();
			}

			//Gets the range of avilables dates in the database
			try
			{
				DatesRangeModel model = await _datesRangeHelper.GetDatesRange();
				StartDate = model.StartDate;
				EndDate = model.EndDate;
				//Initialize DatePiker in end date
				Date = model.EndDate;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, $"Error, could not retrieve avilable dates range, " +
					$"the application may not work correctly");
			}

			CanCallGetRate = true;

			//Finally the call to the API is made
			try
			{
				await GetRateAsync();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error, could not retrieve data from API");
			}
			
		}

       //Sets initial values for comboboxes
		public void  SetSelectedCurrencies ()
		{
			try
			{
				//From App.Config
				SelectedBase = CurrenciesList.FirstOrDefault
					(x => x.Contains(ConfigurationManager.AppSettings["DefaultBase"]));

				SelectedQuote = CurrenciesList.FirstOrDefault
					(x => x.Contains(ConfigurationManager.AppSettings["DefaultQuote"]));

				//Or defaults
				if (SelectedBase == null) 
					SelectedBase = CurrenciesList[0];

				if (SelectedQuote == null)
					SelectedQuote = CurrenciesList[1];
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error, could not retrieve currencies, shutting down...");
				Application.Current.Shutdown();
			}
		}
	


		//Gets rate from API and calls the method that will initiates calculations
		public async Task GetRateAsync()
		{
			if (SelectedBase == SelectedQuote)
			{
				QuoteValue = BaseValue;
				Rate = 1;
			}
			else
			{
				if (Date >= StartDate && Date <= EndDate)
				{
					Rate = await _rateHelper.GetRateAsync(SelectedBase, SelectedQuote, Date);

					if (BaseValue != "")
					{
						//Default is calulate from base value, although user can calculate from quote value using the
						//QuoteValue textbox, and CallCalculator will be called with the "QuoteValue" parameter.
						Calculator("BaseValue");
					}
				}
				else
				{
					//There is a bug when an invalid date is entered, the message error is shown twice
					MessageBox.Show($"No registers for such date. " +
						$"This message may show twice... it's a bug, will be fixed", "Error");

					Date = EndDate;
				}
			}

		}

		public void Calculator(string hasChanged)
		{
			
			try
			{
				//Sees if user wants to calculate from base value or quote value.
				//Checks if the values in the textboxes are correct for calling the calculator method.		
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
				//If not, it clears them.
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


		private DateTime _startDate;

		public DateTime StartDate
		{	
			get { return _startDate; }
			set 
			{ 
				_startDate = value;
				NotifyOfPropertyChange(()=> StartDate);
			}
		}

		private DateTime _endDate;

		public DateTime EndDate
		{
			get { return _endDate; }
			set 
			{
				_endDate = value;
				NotifyOfPropertyChange(() => EndDate);
			}
		}


		public decimal Rate { get; set; }

		public BindableCollection<string> CurrenciesList
		{

			get { return _currenciesList; }

			set 
			{ 
				_currenciesList = value;
				NotifyOfPropertyChange(() => CurrenciesList);
			}
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
