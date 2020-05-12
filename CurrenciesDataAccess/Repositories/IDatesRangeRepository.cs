using CurrenciesDataAccess.Models;
using System;
using System.Threading.Tasks;

namespace CurrenciesDataAccess.Repositories
{
    public interface IDatesRangeRepository: IDisposable
    {
        Task<DatesRange> GetDatesRangeAsync();
    }
}