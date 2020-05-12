using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrenciesLibrary;
using CurrenciesLibrary.CurrenciesUtilities;
using Xunit;


namespace CurrenciesLibrary.Tests.CurrenciesUtilitiesTests
{
    public class CurrenciesCalcultorTests
    {
        [Theory]
        [InlineData("1", 1.3867, 1.3867)]
        [InlineData("15.23", 6.35, 96.7105)]
        [InlineData("7.578945", 4.2437, 32.1628)] // result is 32.162768 etc, but it is rounded.
       
       
        public void CalculateQuoteValue_ShouldCalculate(string baseValue, decimal rate, decimal expected)
        {
      
            decimal actual = CurrenciesCalculator.CalculateQuoteValue(baseValue, rate);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CalculateQuoteValue_ShouldThrowOverflowException()
        {
            Assert.Throws<OverflowException>(() => CurrenciesCalculator.CalculateQuoteValue("99999999999999999999999999", 999999999999999));
        }

        [Fact]
        public void CalculateQuoteValue_ShouldThrowFormatException()
        {
            Assert.Throws<FormatException>(() => CurrenciesCalculator.CalculateQuoteValue("a", 0.5M));
        }

       

        [Theory]
        [InlineData("35",2.5,14)]
        
        public void CalculateBaseValue_ShouldCalculate(string quoteValue, decimal rate, decimal expected)
        {
            decimal actual = CurrenciesCalculator.CalculateBaseValue(quoteValue, rate);
            Assert.Equal(expected, actual);
        }


        [Fact]
        //In case there is some error in the API retrieved values and rate is equal to 0.
        public void CalculateBaseValue_ShouldThrowDivideByZeroException() 
        {
            Assert.Throws<DivideByZeroException>(() => CurrenciesCalculator.CalculateBaseValue("12.6", 0));
        }

        [Fact]
        public void CalculateBaseValue_ShouldThrowOverflowException()
        {
            Assert.Throws<OverflowException>(() => CurrenciesCalculator.CalculateBaseValue("999999999999999999999999999", 0.000000000000000000001M));
        }

        [Fact]
        public void CalculateBaseValue_ShouldThrowsFormatExcpetion()
        {
            Assert.Throws<FormatException>(() => CurrenciesCalculator.CalculateBaseValue("h", 0.3M));
        }
    }
}
