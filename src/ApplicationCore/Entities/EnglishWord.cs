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

        public EnglishWord(string phrase, string transcription, string translation, 
                           string example, string pictureUri, EnglishGroup englishGroup)
        {
            Guard.Against.NullOrEmpty(phrase, nameof(phrase));

            Phrase = phrase;
            Transcription = transcription;
            Translation = translation;
            Example = example;
            PictureUri = pictureUri;
            EnglishGroup = englishGroup;
        }
    }
}
