using CurrenciesDataAccess.Models;
using CurrenciesDataAccess.Repositories;
using CurrenciesDataManagerLibrary.Models;
using CurrenciesDataManagerLibrary.Processor;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;


namespace CurrenciesDataManagerAPI.Controllers
{
    public class RatesController : ApiController
    {
        private readonly IDataProcessor _dataProcessor ;

        
        public RatesController(IDataProcessor dataProcessor)
        {
            _dataProcessor = dataProcessor;
        }

        [Route("{date}")]
        public async Task<CurrenciesRateApiModel> Get(string date)
        {
            try
            {
                var allUrlKeyValues = ControllerContext.Request.GetQueryNameValuePairs();
                string baseCurrency = allUrlKeyValues.LastOrDefault(x => x.Key == "base").Value;
                string quoteCurrency = allUrlKeyValues.LastOrDefault(x => x.Key == "symbols").Value;

                CurrenciesRateApiModel model = await _dataProcessor.GetRateAsync(baseCurrency, quoteCurrency, date);
                return model;
            }
            catch 
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }
    }
}
