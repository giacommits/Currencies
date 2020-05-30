using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCCurrenciesUI.Models
{
    public class CurrenciesViewModel
    {
        public string BaseValue { get; set; } = "1";
        public string QuoteValue { get; set; }
        public string SelectedBase { get; set; }
        public string SelectedQuote { get; set; }      
        public string Rate { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}