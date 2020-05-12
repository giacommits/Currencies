using CurrenciesLibrary.CurrenciesAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CurrenciesLibrary.Tests.CurrenciesAPITests
{
    public class EndpointUrlTests
    {
        [Theory]
        [ClassData(typeof(EndpointUrlTestData))]
        public void GenerateString_ShouldReturnUrl(string baseCurrency, string quoteCurrency, string date, string expected)
        {
            string actual = EndpointUrl.GenerateString(baseCurrency, quoteCurrency, date);
            Assert.Equal(expected, actual);
            
        }   

        public class EndpointUrlTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { "CAD", "EUR", "2019-02-13", "2019-02-13?symbols=EUR&base=CAD" };
                yield return new object[] { "EUR", "USD", DateTime.Now.ToString("yyyy-MM-dd"), "latest?symbols=USD&base=EUR" };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }  
    }

     
}
