using CurrenciesLibrary.CurrenciesAPI;
using CurrenciesLibrary.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MVCCurrenciesUI.Controllers.Helpers
{
	public class DatesRangeHelper : IDatesRangeHelper
	{
		private IAPIHelper _apiHelper;
		public DatesRangeHelper(IAPIHelper apihelper)
		{
			_apiHelper = apihelper;
		}


		public async Task<DatesRangeUIModel> GetDatesRangeAsync()
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