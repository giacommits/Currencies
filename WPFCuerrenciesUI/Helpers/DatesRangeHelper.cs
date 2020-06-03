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


		public async Task<DatesRangeUIModel> GetDatesRange()
		{
			if (_apiHelper.UseLocalApi)
			{
				return await _apiHelper.GetDatesRangeAsync();
			}
			else
			{
				DatesRangeUIModel model = new DatesRangeUIModel
				{
					StartDate = DateTime.Parse(ConfigurationManager.AppSettings["RemoteApiStartDate"]),
					EndDate = DateTime.Now,
				};

				return model;
			}
		}
	}
}
