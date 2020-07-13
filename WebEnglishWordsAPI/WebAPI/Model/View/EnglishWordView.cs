using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using System.Text;

namespace WebAPI.Model
{
    public class EnglishWordView
    {
        public int Id { get; set; }

        public string WordPhrase { get; set; }

        public string Transcription { get; set; }

        public string Translate { get; set; }

        public string Example { get; set; }

        public string CreateDate { get; set; }

        public int ShowCount { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
