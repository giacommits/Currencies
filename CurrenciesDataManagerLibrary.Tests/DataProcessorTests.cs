
using CurrenciesDataManagerLibrary.Entities;
using CurrenciesDataManagerLibrary.Models;
using CurrenciesDataManagerLibrary.Processor;
using CurrenciesDataManagerLibrary.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CurrenciesDataManagerLibrary.Tests
{
    public class DataProcessorTests
    {
        private readonly DataProcessor _sut;
        private readonly Mock<ICurrenciesRateRepository> _currenciesRateRepositoryMock = new Mock<ICurrenciesRateRepository>();
        private readonly Mock<ICurrenciesListRepository> _currenciesListRepositoryMock = new Mock<ICurrenciesListRepository>();
        private readonly Mock<IDatesRangeRepository> _datesRangeRepository = new Mock<IDatesRangeRepository>();
        public DataProcessorTests()
        {
            _sut = new DataProcessor(_currenciesRateRepositoryMock.Object, _currenciesListRepositoryMock.Object,
                _datesRangeRepository.Object);
        }
        [Fact]
        public async  void GetRateAsync_ShouldReturnValidModel() 
        {
            // Arrange
            string baseCurrency = "CAD";
            string quoteCurrency = "EUR";
            string date = "2020-02-15";
            decimal rate = 0.9877M;
            var expected = new CurrenciesRateApiModel()
            {
                Rates = { { quoteCurrency, rate } },
                Base = baseCurrency,
                Date = date
            };
            _currenciesRateRepositoryMock.Setup(x => x.GetRateAsync(baseCurrency, quoteCurrency, date))
                .ReturnsAsync(rate);

            //Act
            var actual = await _sut.GetRateAsync(baseCurrency, quoteCurrency, date);

            //Assert
            Assert.True(actual != null);
            Assert.Equal(expected.Rates, actual.Rates);
            Assert.Equal(expected.Base, actual.Base);
            Assert.Equal(expected.Date, actual.Date);
        }

       [Fact]
       public async void GetRateAsync_ShouldThrowException()
        {
            
            _currenciesRateRepositoryMock.Setup(x => x.GetRateAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
              .ReturnsAsync(()=> 0.0M);

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _sut
            .GetRateAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

        }

        [Fact]
        public async void GetCurrenciesListAsync_ShouldReturnDictionary()
        {
            //Arrange
            List<Currency> list = new List<Currency>()
            {
                new Currency
                {
                    ISO_Code = "CAD",
                    Name ="Canadian Dollar",
                },

                new Currency
                {
                    ISO_Code = "HKD",
                    Name = "Hong Kong Dollar",
                }
            };

            _currenciesListRepositoryMock.Setup(x => x.GetCurrenciesListAsync()).ReturnsAsync(list);

            Dictionary<string, string> expected = new Dictionary<string, string>
                    {
                        { "CAD",   "Canadian Dollar" },
                        {"HKD",    "Hong Kong Dollar" }
                    };

            //Act
            var actual = await  _sut.GetCurrenciesListAsync();

            //Assert
            Assert.Equal(expected, actual);

        }

        [Fact]
        public async void  GetCurrenciesListAsync_ShouldThrowException()
        {
            _currenciesListRepositoryMock.Setup(x => x.GetCurrenciesListAsync()).ReturnsAsync(()=> null);
           
           await Assert.ThrowsAsync<NullReferenceException>(async ()=> await _sut.GetCurrenciesListAsync());
        }
    }
}
