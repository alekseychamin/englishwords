using PublicApi.Endpoints.EnglishGroups;

namespace PublicApi.Models
{
    public class EnglishWordDto
    {
        public int Id { get; set; }

        public string Phrase { get; set; }

        public string Transcription { get; set; }

        public string Translation { get; set; }

        public string Example { get; set; }

        public string PictureUri { get; set; }

        public string CreateDate { get; set; }

        public EnglishGroupDto EnglishGroup { get; set; }
    }
}
