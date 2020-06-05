using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CurrenciesLibrary.CurrenciesUtilities
{
    //When using remote API, gets a Dictionary with the avilables currencies ("CAD": "Canadian Dollars") from a json file 
    //since the remote API doesn't have such endpoint.
    public class CurrenciesList : ICurrenciesList   
    {
        //Implementation of System.IO.Abstractions for testings purposes
        IFileSystem _fileSystem;
        public CurrenciesList() : this(new FileSystem()) { }        

        public CurrenciesList(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }
        public async Task<Dictionary<string, string>> GetCurrenciesListAsync()
        {
            //This is for accesing the file when the Library is used both from a desktop app, and a web app.
            var appDomain = AppDomain.CurrentDomain;
            var basePath = appDomain.RelativeSearchPath ?? appDomain.BaseDirectory;
            var filePath = Path.Combine(basePath, "dictionary.json");


            if (_fileSystem != null)
            {
            //For some reason using  _fileSystem, while it works fine when the Library is called from the MVCCurrenciesUI project,
            //it returns System.NullReferenceException when called from the WPFCurrenciesUI project,
            //so for now we use this workaround.

                using (var reader = _fileSystem.File.OpenText(filePath))
                {
                    var fileText = await reader.ReadToEndAsync();
                    return JsonConvert.DeserializeObject<Dictionary<string, string>>(fileText);
                }
            }
            else
            {
                using (var reader = File.OpenText(filePath))
                {
                    var fileText = await reader.ReadToEndAsync();
                    return JsonConvert.DeserializeObject<Dictionary<string, string>>(fileText);
                }
            }

            
        }

    }
}
