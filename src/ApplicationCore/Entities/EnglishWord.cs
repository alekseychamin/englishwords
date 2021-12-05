using ApplicationCore.Entities.Dto;
using ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using System;

namespace ApplicationCore.Entities
{
    public class EnglishWord : BaseEntity, IAggregateRoot
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

        public EnglishWord(EnglishWordCoreDto englishWord)
        {
            Guard.Against.NullOrEmpty(englishWord.Phrase, nameof(englishWord.Phrase));

            Phrase = englishWord.Phrase;
            Transcription = englishWord.Transcription;
            Translation = englishWord.Translation;
            Example = englishWord.Example;
            PictureUri = englishWord.PictureUri;
            EnglishGroup = englishWord.EnglishGroup;
        }

        public void Update()
        {

        }
    }
}
