using CurrenciesLibrary.CurrenciesAPI;
using CurrenciesLibrary.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFCuerrenciesUI.Helpers
{
	public class DatesRangeHelper : IDatesRangeHelper
	{
		private IAPIHelper _apiHelper;
		public DatesRangeHelper(IAPIHelper apihelper)
		{
			_apiHelper = apihelper;
		}


		public async Task<DatesRangeModel> GetDatesRange()
		{
			if (_apiHelper.InternalApi)
			{
				return await _apiHelper.GetDatesRangeAsync();
			}
			else
			{
				DatesRangeModel model = new DatesRangeModel
				{
					StartDate = DateTime.Parse(ConfigurationManager.AppSettings["PublicApiStartDate"]),
					EndDate = DateTime.Now,
				};

				return model;
			}
		}
	}
}
