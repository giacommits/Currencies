using CurrenciesLibrary.Models;
using System.Threading.Tasks;

namespace MVCCurrenciesUI.Controllers.Helpers
{
    public interface IDatesRangeHelper
    {
        Task<DatesRangeUIModel> GetDatesRangeAsync();
    }
}