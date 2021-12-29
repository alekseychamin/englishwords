using System.ComponentModel.DataAnnotations;

namespace PublicApi.Endpoints.EnglishWords
{
    public class CreateEnglishWordRequest
    {
        [Required]
        public string Phrase { get; set; }

        public string Transcription { get; set; }

        public string Translation { get; set; }

        public string Example { get; set; }

        public int? EnglishGroupId { get; set; }
    }
}