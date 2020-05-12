using CurrenciesDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrenciesDataAccess.Repositories
{
    public class CurrenciesRepository : ICurrenciesRepository
    {
        private readonly CurrenciesDb _context;

        public CurrenciesRepository(CurrenciesDb context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<List<Currency>> GetCurrenciesListAsync()
        {
            return await _context.Currencies.ToListAsync();
        }
    }
}
