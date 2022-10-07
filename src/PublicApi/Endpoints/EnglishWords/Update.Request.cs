using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.Endpoints.EnglishWords
{
    public class UpdateEnglishWordRequest
    {
        public int Id { get; set; }

        [Required]
        public string Phrase { get; set; }

        public string Transcription { get; set; }

        public string Translation { get; set; }

        public string Example { get; set; }

        public string PictureUri { get; set; }

        public int? EnglishGroupId { get; set; }
        
        public DateTime CreateDate { get; set; }
    }
}