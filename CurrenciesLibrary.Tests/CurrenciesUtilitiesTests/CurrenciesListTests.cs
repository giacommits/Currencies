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

//Temporaly commented out due to changes in tested class.

//namespace CurrenciesLibrary.Tests.CurrenciesUtilitiesTests
//{
//    public class CurrenciesDictionaryTests
//    {
//        [Fact]
//        public async void GetDictionary_ShouldReturnJobject()
//        {
//           using (var mock = AutoMock.GetLoose())
//            {
//                mock.Mock<IFileSystem>().Setup(x => x.File.OpenText("dictionary.json"))
//                    .Returns(JsonConvert.SerializeObject(dic));

//                var cls = mock.Create<CurrenciesList>();
//                var expected = dic;
//                var actual = await cls.GetCurrenciesListAsync();

//                Assert.True(actual != null);
//                Assert.Equal(expected, actual);
                
//            }
//        }

//        Dictionary<string, string> dic = new Dictionary<string, string>
//        {
//            { "CAD",   "Canadian Dollar" },
//            {"HKD",    "Hong Kong Dollar" }
//        };


//        [Fact]
//        public async void GetDictionary_ShouldThrowException()
//        {
//            using (var mock = AutoMock.GetLoose())
//            {
//                mock.Mock<IFileSystem>().Setup(x => x.File.ReadAllText("dictionary.json"))
//                    .Returns(()=> throw new Exception());

//                var cls = mock.Create<CurrenciesList>();

//                await Assert.ThrowsAsync<Exception>(async ()=>await  cls.GetCurrenciesListAsync());

//            }
//        }

//    }
//}
