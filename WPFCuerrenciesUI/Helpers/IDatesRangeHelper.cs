using CurrenciesLibrary.Models;
using System.Threading.Tasks;

namespace WPFCuerrenciesUI.Helpers
{
    public interface IDatesRangeHelper
    {
        Task<DatesRangeModel> GetDatesRange();
    }
}