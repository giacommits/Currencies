
using CurrenciesDataManagerLibrary.Models;
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
    public class DatesRangeController : ApiController
    {
        IDataProcessor _dataProcessor;

        public DatesRangeController(IDataProcessor dataProcessor)
        {
            _dataProcessor = dataProcessor;
        }

        [Route("dates/range")]
        public async Task<DatesRangeApiModel> Get()
        {
            try
            {
                return await _dataProcessor.GetDatesRangeAsync();
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

       
    }
}
