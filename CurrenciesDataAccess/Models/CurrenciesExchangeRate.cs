//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CurrenciesDataAccess.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CurrenciesExchangeRate
    {
        public int CurrenciesExchangeRateId { get; set; }
        public System.DateTime RateDate { get; set; }
        public decimal ExchangeRate { get; set; }
    
        public virtual Currency BaseCurrency { get; set; }
        public virtual Currency QuoteCurrency { get; set; }
    }
}
