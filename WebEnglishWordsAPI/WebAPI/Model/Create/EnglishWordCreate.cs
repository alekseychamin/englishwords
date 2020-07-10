using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using System.Text;

namespace WebAPI.Model
{
    public class EnglishWordCreate
    {
        public string WordPhrase { get; set; }

        public string Transcription { get; set; }

        public string Translate { get; set; }

        public string Example { get; set; }

        //public DateTime CreateDate { get; set; } = DateTime.Now;

        //public int ShowCount { get; set; }
        
        public int CategoryId { get; set; }
    }
}
