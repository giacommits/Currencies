using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrenciesLibrary.CurrenciesUtilities
{
    public interface ICurrenciesList
    {
        Task<Dictionary<string, string>> GetCurrenciesListAsync();
    }
}