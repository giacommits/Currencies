using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrenciesLibrary.CurrenciesUtilities
{
    //Get a Dictionary with the avilables currencies ("CAD": "Canadian Dollars") from a json file 
    //since the API doesn't have such endpoint.
    public class CurrenciesList : ICurrenciesList   
    {
        //Implementation of System.IO.Abstractions for testings purposes
        IFileSystem _fileSystem;
        public CurrenciesList() : this (new FileSystem()) { }
        

        public CurrenciesList(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }
        public async Task<Dictionary<string, string>> GetCurrenciesListAsync()
        {
            //For some reason _fileSystem.File.ReadAllText("dictionary.json") while it works for the Tests project,
            //returns a null object exception for the UI project, so we are using File.ReadAllText instead till 
            //the bug is fixed. Tests for this class are temporaly commented out.

            

            using (var reader = File.OpenText("dictionary.json"))
            {
                var fileText = await reader.ReadToEndAsync();
                // Do something with fileText...
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(fileText);
            }

            
        }

    }
}
