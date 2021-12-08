using ApplicationCore.Interfaces;

namespace ApplicationCore.Entities.Dto
{
    public class EnglishWordCoreDto : IBaseCoreDto
    {
        public int Id { get; set; }

        public string Phrase { get; set; }

        public string Transcription { get; set; }

        public string Translation { get; set; }

        public string Example { get; set; }

        public string PictureUri { get; set; }

        public string CreateDate { get; set; }

        public int? EnglishGroupId { get; set; }

        public EnglishGroup EnglishGroup { get; set; }
    }
}
