using PublicApi.Models;
using System.Collections.Generic;

namespace PublicApi.EnglishGroupEndpoints
{
    public class GetByIdEnglishGroupResult
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<EnglishWordDto> EnglishWords { get; set; }
    }
}