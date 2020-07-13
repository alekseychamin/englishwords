using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTelegramBot
{
    public class EnglishWord
    {
        public int id { get; set; }
        public string wordPhrase { get; set; }
        public string transcription { get; set; }
        public string translate { get; set; }
        public string example { get; set; }
        public string createDate { get; set; }
        public int showCount { get; set; }
        public int categoryId { get; set; }
        public string categoryName { get; set; }
    }
}
