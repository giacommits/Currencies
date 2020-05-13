using Caliburn.Micro;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WPFCuerrenciesUI.Helpers
{
    public interface ICurrenciesListHelper
    {
        Task<BindableCollection<string>> GetCurrenciesListAsync();
    }
}