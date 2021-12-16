using ApplicationCore.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ApplicationCore.Entities.Dto
{
    public class EnglishGroupCoreDto : IBaseCoreDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public List<EnglishWordCoreDto> EnglishWords { get; set; }

        public TypeOperation Type { get; set; } = TypeOperation.None;
    }
}
