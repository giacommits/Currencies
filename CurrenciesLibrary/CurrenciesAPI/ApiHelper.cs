
using CurrenciesLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CurrenciesLibrary.CurrenciesAPI
{
    public class APIHelper : IAPIHelper
    {
        public HttpClient ApiClient { get; set; }
        public APIHelper()
        {
            InitializeClient();
        }

        public bool LocalApi { get; set; }
        private void InitializeClient()
        {
            string api = ConfigurationManager.AppSettings["localApi"];
            if (api == "")
            {
                api = ConfigurationManager.AppSettings["remoteApi"];
            }
            else
            {
                LocalApi = true;
            }

            ApiClient = new HttpClient();
            ApiClient.BaseAddress = new Uri(api);
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        //Calls the API and gets the data in the Model.
        public async Task<CurrenciesRateAPIModel> GetRateFromAPIAsync(string baseCurrency, string quoteCurrency, string date)
        {

            string url = EndpointUrl.GenerateString(baseCurrency, quoteCurrency, date);


            using (HttpResponseMessage response = await ApiClient.GetAsync(url))
            {

                if (response.IsSuccessStatusCode)
                {
                    CurrenciesRateAPIModel rate = await response.Content.ReadAsAsync<CurrenciesRateAPIModel>();
                    return rate;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }

            }
        }

        public async Task<Dictionary<string,string>> GetCurrenciesListFromApiAsync()
        {
            using (HttpResponseMessage response = await ApiClient.GetAsync("currencies/list"))
            {

                if (response.IsSuccessStatusCode)
                {
                    var temp = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Dictionary<string, string>>(temp);
                    
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }

            }
        }

        public async Task<DatesRangeApiModel> GetDatesRangeAsync()
        {
            using (HttpResponseMessage response = await ApiClient.GetAsync("dates/range"))
            {

                if (response.IsSuccessStatusCode)
                {
                    var temp = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<DatesRangeApiModel>(temp);

                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }

            }
        }
       
    }
}
