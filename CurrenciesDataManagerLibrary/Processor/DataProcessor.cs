using CurrenciesDataManagerLibrary.Repositories;
using CurrenciesDataManagerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrenciesDataManagerLibrary.Processor
{
    public class DataProcessor : IDataProcessor
    {
        ICurrenciesRateRepository _currenciesRateRepository;
        ICurrenciesListRepository _currenciesListRespository;
        IDatesRangeRepository _datesRangeRepository;

        public DataProcessor(ICurrenciesRateRepository currenciesRateRepository,
            ICurrenciesListRepository currenciesListRepository,
            IDatesRangeRepository datesRangeRepository)
        {
            _currenciesRateRepository = currenciesRateRepository;
            _currenciesListRespository = currenciesListRepository;
            _datesRangeRepository = datesRangeRepository;
        }

        public async Task<CurrenciesRateApiModel> GetRateAsync(string baseCurrency, string quoteCurrency, string date)
        {

            decimal rate = await _currenciesRateRepository.GetRateAsync(baseCurrency, quoteCurrency, date);
            CurrenciesRateApiModel apiModel = new CurrenciesRateApiModel();
            apiModel.rates.Add(baseCurrency, rate);
            apiModel.Base = baseCurrency;
            apiModel.Date = date;

            return apiModel;
        }

        public async Task<Dictionary<string, string>> GetCurrenciesListAsync()         
        {
            Dictionary<string, string> CurrenciesList = new Dictionary<string, string>();
            var result = await _currenciesListRespository.GetCurrenciesListAsync();

            foreach (var currency in result)
            {
                CurrenciesList.Add(currency.ISO_Code, currency.Name);
            }

            return CurrenciesList;
        }

        public async Task<DatesRangeApiModel> GetDatesRangeAsync()
        {
           
            var result = await _datesRangeRepository.GetDatesRangeAsync();
            DatesRangeApiModel apiModel = new DatesRangeApiModel
            {
                StartDate = result.StartDate,
                EndDate = result.EndDate,
            };

            return apiModel;
        }

    }
}
