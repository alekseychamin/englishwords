using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTelegramBot.Model
{
    public class NewEnglishWord
    {
        public string WordPhrase { get; set; }
        public string Transcription { get; set; }
        public string Translate { get; set; }
        public string Example { get; set; }
        public int CategoryId { get; set; }
    }
}
