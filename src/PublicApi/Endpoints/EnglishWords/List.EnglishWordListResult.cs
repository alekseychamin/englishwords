using PublicApi.Models;
using System.Collections.Generic;

namespace PublicApi.Endpoints.EnglishWords
{
    public class EnglishWordListResult
    {
        public List<EnglishWordDto> EnglishWords { get; set; }
    }
}