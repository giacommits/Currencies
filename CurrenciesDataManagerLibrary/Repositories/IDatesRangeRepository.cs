using CurrenciesDataManagerLibrary.Models;
using System;
using System.Threading.Tasks;

namespace CurrenciesDataManagerLibrary.Repositories
{
    public interface IDatesRangeRepository: IDisposable
    {
        Task<DatesRangeApiModel> GetDatesRangeAsync();
    }
}