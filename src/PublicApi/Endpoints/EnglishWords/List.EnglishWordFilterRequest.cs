using PublicApi.Models;

namespace PublicApi.Endpoints.EnglishWords
{
    public class EnglishWordFilterRequest : BaseFilterDto
    {
        public int? EnglishGroupId { get; set; }

        public bool SearchingAllGroups { get; set; }

        public string Phrase { get; set; }

        public string Transcription { get; set; }

        public string Translation { get; set; }

        public string Example { get; set; }

        public string LessThanCreatedDate { get; set; }

        public string MoreThanCreatedDate { get; set; }
    }
}