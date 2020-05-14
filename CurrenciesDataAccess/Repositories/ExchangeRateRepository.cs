using CurrenciesDataAccess.Models;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace CurrenciesDataAccess.Repositories
{
    public class ExchangeRateRepository : IExchangeRateRepository
    {
        private readonly CurrenciesDb _context;

        public ExchangeRateRepository(CurrenciesDb context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<decimal> GetRateAsync(string baseCurrency, string quoteCurrency, string date)
        {
            var temp = DateTime.Parse(date);
            var result = await _context.CurrenciesExchangeRates.FirstOrDefaultAsync(x => x.BaseCurrency.ISO_Code == baseCurrency
            && x.QuoteCurrency.ISO_Code.Equals(quoteCurrency) && x.RateDate == temp);
            return result.ExchangeRate;
        }

    }




}
