﻿using ApplicationCore.Entities.Dto;
using ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using Newtonsoft.Json;
using System;
using System.Globalization;

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

            entityDto.Type = TypeOperation.Create;
            SetProperties(entityDto);
        }

        public override void Update(EnglishWordCoreDto entityDto)
        {
            Guard.Against.NullOrEmpty(entityDto.Phrase, nameof(entityDto.Phrase));

            entityDto.Type = TypeOperation.Update;
            SetProperties(entityDto);
        }

        protected override void SetProperties(EnglishWordCoreDto entityDto)
        {
            Id = entityDto.Id;
            Phrase = entityDto.Phrase;
            Transcription = entityDto.Transcription;
            Translation = entityDto.Translation;
            Example = entityDto.Example;
            PictureUri = entityDto.PictureUri;
            CreateDate = DateTime.TryParseExact(entityDto.CreateDate,
                                                "MM/dd/yyyy",
                                                CultureInfo.InvariantCulture,
                                                DateTimeStyles.None,
                                                out DateTime date) ? date : (entityDto.Type == TypeOperation.Create) ? DateTime.Today : CreateDate;
            EnglishGroupId = entityDto.EnglishGroupId;
            EnglishGroup = entityDto.EnglishGroup;
        }
    }
}
