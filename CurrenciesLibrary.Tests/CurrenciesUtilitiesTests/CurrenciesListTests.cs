using Autofac.Extras.Moq;
using CurrenciesLibrary.CurrenciesUtilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xunit;
using Xunit.Sdk;

namespace CurrenciesLibrary.Tests.CurrenciesUtilitiesTests
{
    //
    // This test is currently not avilable due to changes in the class that it reference
    // 
   
    /*
    public class CurrenciesDictionaryTests
    {
        [Fact]
        public void GetDictionary_ShouldReturnJobject()
        {
           using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IFileSystem>().Setup(x => x.File.ReadAllText("dictionary.json"))
                    .Returns(JsonConvert.SerializeObject(dic));

                var cls = mock.Create<CurrenciesDictionary>();
                var expected = dic;
                var actual = cls.GetDictionary();

                Assert.True(actual != null);
                Assert.Equal(expected, actual);
                
            }
        }

        Dictionary<string, string> dic = new Dictionary<string, string>
        {
            { "CAD",   "Canadian Dollar" },
            {"HKD",    "Hong Kong Dollar" }
        };


        [Fact]
        public void GetDictionary_ShouldThrowException()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IFileSystem>().Setup(x => x.File.ReadAllText("dictionary.json"))
                    .Returns(()=> throw new Exception());

                var cls = mock.Create<CurrenciesDictionary>();

                Assert.Throws<Exception>(()=> cls.GetDictionary());

            }
        }

    }*/
}
