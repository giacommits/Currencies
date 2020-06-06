using CurrenciesLibrary.CurrenciesUtilities;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CurrenciesLibrary.Tests.CurrenciesUtilitiesTests
{
    public class CurrenciesListTests
    {
        private readonly CurrenciesList _sut;
        private readonly Mock<IFileSystem> _fileSystemMock = new Mock<IFileSystem>();

        public CurrenciesListTests()
        {
            _sut = new CurrenciesList(_fileSystemMock.Object);
        }

        [Fact]
        public async void GetCurrenciesListAsync_ShouldReturnCurrenciesDictionary()
        {
            //Arrange
            var appDomain = AppDomain.CurrentDomain;
            var basePath = appDomain.RelativeSearchPath ?? appDomain.BaseDirectory;
            var filePath = Path.Combine(basePath, "dictionary.json");
            string fakeFileContent = "{\"CAD\": \"Canadian Dollar\", \"HKD\": \"Hong Kong Dollar\"}";
            byte[] fakeFileBytes = Encoding.UTF8.GetBytes(fakeFileContent);
            MemoryStream fakeMemoryStream = new MemoryStream(fakeFileBytes);
            StreamReader fakeStreamReader = new StreamReader(fakeMemoryStream);

            var expected = new Dictionary<string, string>()
            {
                { "CAD", "Canadian Dollar" },
                { "HKD", "Hong Kong Dollar" }
            };

            _fileSystemMock.Setup(x => x.File.OpenText(filePath))
                .Returns(fakeStreamReader);


            //Act
            var actual = await _sut.GetCurrenciesListAsync();

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
