using PublicApi.Models;

namespace PublicApi.Endpoints.EnglishWords
{
    public class UpdateEnglishWordResult
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