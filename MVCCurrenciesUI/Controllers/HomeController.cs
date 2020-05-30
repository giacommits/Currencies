
using CurrenciesLibrary.Models;
using MVCCurrenciesUI.Controllers.Helpers;
using MVCCurrenciesUI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVCCurrenciesUI.Controllers
{
    public class HomeController : Controller
    {
        ICurrenciesListHelper _currenciesListHelper;
        IDatesRangeHelper _datesRangeHelper;
        IRateHelper _rateHelper;
        public HomeController(ICurrenciesListHelper currenciesListHelper, IDatesRangeHelper datesRangeHelper,
            IRateHelper rateHelper)
        {
            _currenciesListHelper = currenciesListHelper;
            _datesRangeHelper = datesRangeHelper;
            _rateHelper = rateHelper;
        }

        public async Task<ActionResult> Index(CurrenciesViewModel model)
        {

            ModelState.Clear();
            if (model.Rate == null)
            {
                List<string> currenciesList = await _currenciesListHelper.GetCurrenciesListAsync();               
               
                model.SelectedBase = currenciesList.FirstOrDefault
                         (x => x.Contains(ConfigurationManager.AppSettings["DefaultBase"]));

                model.SelectedQuote = currenciesList.FirstOrDefault
                        (x => x.Contains(ConfigurationManager.AppSettings["DefaultQuote"]));

                //Or defaults
                if (model.SelectedBase == null)
                    model.SelectedBase = currenciesList[0];

                if (model.SelectedQuote == null)
                    model.SelectedQuote = currenciesList[1];

                DatesRangeUIModel datesRangeModel = await _datesRangeHelper.GetDatesRangeAsync();
                model.StartDate = DateTime.Parse(datesRangeModel.StartDate.ToString("dd-MM-yyyy"));
                model.EndDate = DateTime.Parse(datesRangeModel.EndDate.ToString("dd-MM-yyyy"));
                model.Date = model.EndDate;
                Session["CurrenciesList"] = currenciesList;
            }


            model.Rate = await _rateHelper.GetRateAsync(model.SelectedBase, model.SelectedQuote, model.Date);
           
            //System.Threading.Thread.Sleep(4000);
            return View(model);
        }

        
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public async Task<ContentResult> GetRate(CurrenciesViewModel model)
        {
            ModelState.Clear();
            string rate = await _rateHelper.GetRateAsync(model.SelectedBase, model.SelectedQuote, model.Date);
                        
            return Content(rate);
        }
    }
}