using Caliburn.Micro;
using CurrenciesLibrary.Models;
using CurrenciesLibrary.CurrenciesUtilities;
using System;
using System.Globalization;
using System.Linq;
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
		private ICurrenciesListHelper _currenciesListHelper;
		private IRateHelper _rateHelper;
		private DateTime _date;
		private IDatesRangeHelper _datesRangeHelper;
		private DateTime _startDate;
		private DateTime _endDate;
		private bool _textBoxesAreEnabled;
		private bool _comboBoxesAndDatePickerAreEnabled;
		private BindableCollection<string> _currenciesList = new BindableCollection<string>();

		public ShellViewModel( IDatesRangeHelper dateRangeHelper,
			ICurrenciesListHelper currenciesListHelper,	IRateHelper rateHelper)
		{			
			_datesRangeHelper = dateRangeHelper;
			_currenciesListHelper = currenciesListHelper;
			_rateHelper = rateHelper;
			Calculate = new CalculateCommand(Calculator);
			CallGetRateAsync = new GetRateCommand(GetRateAsync, GetRateAsyncCanExecute);			
		}

		//To avoid calling async method on constructor
		protected async override void OnViewLoaded(object view)
		{
			base.OnViewLoaded(view);

			//Changes in controls automatically calls the API for updating values, setting this property to false avoids it
			CanCallGetRateAsync = false;

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
				DatesRangeUIModel model = await _datesRangeHelper.GetDatesRange();
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

			CanCallGetRateAsync = true;

			//Finally the call to the API is made
			await GetRateAsync();
					
		}

       //Sets initial values for comboboxes
		public void  SetSelectedCurrencies ()
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
	


		//Gets rate from API and calls the method that will initiates calculations
		public async Task GetRateAsync()
		{
			TextBoxesAreEnabled = false;
			ComboBoxesAndDatePickerAreEnabled = false;

			if (SelectedBase == SelectedQuote)
			{
				QuoteValue = BaseValue;
				Rate = 1;
			}
			else
			{
				if (BaseValue == "")
				{
					BaseValue = "1";
				}
				try
				{
					Rate = await _rateHelper.GetRateAsync(SelectedBase, SelectedQuote, Date);
					//Default is calulate from base value, although user can calculate from quote value using the
					//QuoteValue textbox, and CallCalculator will be called with the "QuoteValue" parameter.
					Calculator("BaseValue");
					TextBoxesAreEnabled = true;
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Error, could not retrieve data from API");
					BaseValue = "1";
					QuoteValue = "";
				}

				ComboBoxesAndDatePickerAreEnabled = true;
			}
		}

		public void Calculator(string hasChanged)
		{
			
			try
			{
				//Sees if user wants to calculate from base value or quote value.
				//Checks if the values in the textboxes are correct for calling the calculate method.		
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

		public decimal Rate { get; set; }

		public CalculateCommand Calculate { get; private set; }

		public GetRateCommand CallGetRateAsync { get; private set; }


		public bool CanCallGetRateAsync { get; set; } = true;
		public bool GetRateAsyncCanExecute()
		{
			return CanCallGetRateAsync;
		}
		

		public DateTime StartDate
		{	
			get { return _startDate; }
			set 
			{ 
				_startDate = value;
				NotifyOfPropertyChange(()=> StartDate);
			}
		}
		

		public DateTime EndDate
		{
			get { return _endDate; }
			set 
			{
				_endDate = value;
				NotifyOfPropertyChange(() => EndDate);
			}
		}
		

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

		public bool TextBoxesAreEnabled
		{
			get { return _textBoxesAreEnabled; }
			set
			{
				_textBoxesAreEnabled = value;

				NotifyOfPropertyChange(() => TextBoxesAreEnabled);
			}
		}		

		public bool ComboBoxesAndDatePickerAreEnabled
		{
			get { return _comboBoxesAndDatePickerAreEnabled; }
			set 
			{
				_comboBoxesAndDatePickerAreEnabled = value;

				NotifyOfPropertyChange(() => ComboBoxesAndDatePickerAreEnabled);
			}
		}
	}
}
