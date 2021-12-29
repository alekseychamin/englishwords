using ApplicationCore.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace ApplicationCore.Entities.Seeds
{
    public class SeedFromJsonEnglishWord : ISeed
    {
        private readonly string _fileName;

        public SeedFromJsonEnglishWord(string fileName)
        {
            _fileName = fileName;
        }

        public List<EnglishGroup> GetEnglishGroups()
        {
            var json = File.ReadAllText(_fileName);
            
            return JsonConvert.DeserializeObject<List<EnglishGroup>>(json);
        }
    }
}
