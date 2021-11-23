using ApplicationCore.Interfaces;
using System;

namespace ApplicationCore.Entities
{
    public class EnglishWord : BaseEntity, IAggregateRoot
    {
        public string Phrase { get; set; }

        public string Transcription { get; set; }

        public string Translation { get; set; }

        public string Example { get; set; }

        public string PictureUri { get; set; }

        public int EnglishGroupId { get; set; }

        public EnglishGroup EnglishGroup { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
