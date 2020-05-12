using CurrenciesDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrenciesDataAccess.Repositories
{
    public interface ICurrenciesRepository : IDisposable
    {
        Task<List<Currency>> GetCurrenciesListAsync();
    }
}