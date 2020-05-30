using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCCurrenciesUI.Controllers.Helpers
{
    public interface ICurrenciesListHelper
    {
        Task<List<string>> GetCurrenciesListAsync();
    }
}