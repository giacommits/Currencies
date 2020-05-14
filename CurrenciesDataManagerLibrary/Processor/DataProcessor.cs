using CurrenciesDataAccess.Models;
using CurrenciesDataAccess.Repositories;
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
        ICurrenciesRateRepository _exchangeRateRepository;
        ICurrenciesRepository _currenciesRespository;
        DatesRangeRepository _rangeDatesRepository;

        public DataProcessor(ICurrenciesRateRepository exchangeRateRepository,
            ICurrenciesRepository currenciesRepository,
            DatesRangeRepository minAndMaxDateRepository)
        {
            _exchangeRateRepository = exchangeRateRepository;
            _currenciesRespository = currenciesRepository;
            _rangeDatesRepository = minAndMaxDateRepository;
        }

        public async Task<CurrenciesRateApiModel> GetRateAsync(string baseCurrency, string quoteCurrency, string date)
        {

            decimal rate = await _exchangeRateRepository.GetRateAsync(baseCurrency, quoteCurrency, date);
            CurrenciesRateApiModel apiModel = new CurrenciesRateApiModel();
            apiModel.rates.Add(baseCurrency, rate);
            apiModel.Base = baseCurrency;
            apiModel.Date = date;

            return apiModel;
        }

        public async Task<Dictionary<string, string>> GetCurrenciesListAsync()         
        {
            Dictionary<string, string> CurrenciesList = new Dictionary<string, string>();
            var result = await _currenciesRespository.GetCurrenciesListAsync();

            foreach (var currency in result)
            {
                CurrenciesList.Add(currency.ISO_Code, currency.Name);
            }

            return CurrenciesList;
        }

        public async Task<DatesRangeApiModel> GetDatesRangeAsync()
        {
           
            var result = await _rangeDatesRepository.GetDatesRangeAsync();
            DatesRangeApiModel apiModel = new DatesRangeApiModel
            {
                StartDate = result.StartDate,
                EndDate = result.EndDate,
            };

            return apiModel;
        }

    }
}
