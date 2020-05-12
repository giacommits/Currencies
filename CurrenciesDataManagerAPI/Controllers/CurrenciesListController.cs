using CurrenciesDataManagerLibrary.Processor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CurrenciesDataManagerAPI.Controllers
{
    public class CurrenciesListController : ApiController
    {
        private readonly IDataProcessor _dataProcessor;


        public CurrenciesListController(IDataProcessor dataProcessor)
        {
            _dataProcessor = dataProcessor;

        }

        [Route("currencies/list")]
        public async Task<Dictionary<string,string>> Get()
        {
            try
            {
                var temp = await _dataProcessor.GetCurrenciesListAsync();
                return temp;
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }
       
    }
}
