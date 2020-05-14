using CurrenciesDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrenciesDataAccess.Repositories
{
    public class DatesRangeRepository : IDatesRangeRepository
    {
        private readonly CurrenciesDb _context;

        public DatesRangeRepository(CurrenciesDb context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<DatesRange> GetDatesRangeAsync()
        {
            DateTime StartDate = await _context.CurrenciesRates.MinAsync(x => x.RateDate);
            DateTime EndDate = await _context.CurrenciesRates.MaxAsync(x => x.RateDate);
            DatesRange Dates = new DatesRange
            {
                StartDate = StartDate,
                EndDate = EndDate,
            };

            return Dates;
        }
    }
}
