using ApplicationCore.Entities.Dto;
using ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using System;

namespace ApplicationCore.Entities
{
    public class EnglishWord : BaseEntity<EnglishWordCoreDto>, IAggregateRoot
    {
        public string Phrase { get; private set; }

        public string Transcription { get; private set; }

        public string Translation { get; private set; }

        public string Example { get; private set; }

        public string PictureUri { get; private set; }

        public int? EnglishGroupId { get; private set; }

        public EnglishGroup EnglishGroup { get; private set; }

        public DateTime CreateDate { get; private set; } = DateTime.Now;

        private EnglishWord()
        {

        }

        public EnglishWord(EnglishWordCoreDto entityDto)
        {
            Guard.Against.NullOrEmpty(entityDto.Phrase, nameof(entityDto.Phrase));

            SetProperties(entityDto);
        }

        public override void Update(EnglishWordCoreDto entityDto)
        {
            Guard.Against.NullOrEmpty(entityDto.Phrase, nameof(entityDto.Phrase));

            SetProperties(entityDto);
        }

        protected override void SetProperties(EnglishWordCoreDto entityDto)
        {
            Phrase = entityDto.Phrase;
            Transcription = entityDto.Transcription;
            Translation = entityDto.Translation;
            Example = entityDto.Example;
            PictureUri = entityDto.PictureUri;
            EnglishGroup = entityDto.EnglishGroup;
        }
    }
}
