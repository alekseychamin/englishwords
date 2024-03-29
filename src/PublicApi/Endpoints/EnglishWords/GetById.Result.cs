﻿using PublicApi.Models;
using System;

namespace PublicApi.Endpoints.EnglishWords
{
    public class GetByIdEnglishWordResult
    {
        public string Phrase { get; set; }

        public string Transcription { get; set; }

        public string Translation { get; set; }

        public string Example { get; set; }

        public string PictureUri { get; set; }

        public EnglishGroupDto EnglishGroup { get; set; }

        public DateTime CreateDate { get; set; }
    }
}