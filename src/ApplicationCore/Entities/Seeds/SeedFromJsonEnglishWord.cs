using ApplicationCore.Entities.Dto;
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

        public List<EnglishGroup> Seed()
        {
            var json = File.ReadAllText(_fileName);
            
            var englishGroupsDto = JsonConvert.DeserializeObject<List<EnglishGroupCoreDto>>(json);

            return Map(englishGroupsDto);
        }

        private List<EnglishGroup> Map(List<EnglishGroupCoreDto> englishGroupsDto)
        {
            var result = new List<EnglishGroup>();

            foreach (var englishGroupDto in englishGroupsDto)
            {
                var englishGroup = new EnglishGroup(englishGroupDto);

                foreach (var englishWordDto in englishGroupDto.EnglishWords)
                {
                    englishGroup.AddItem(new EnglishWord(englishWordDto));
                }

                result.Add(englishGroup);
            }

            return result;
        }
    }
}
