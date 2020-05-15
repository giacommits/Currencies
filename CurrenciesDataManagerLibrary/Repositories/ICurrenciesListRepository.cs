using CurrenciesDataManagerLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrenciesDataManagerLibrary.Repositories
{
    public interface ICurrenciesListRepository : IDisposable
    {
        Task<List<Currency>> GetCurrenciesListAsync();
    }
}